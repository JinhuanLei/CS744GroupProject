///var timeouts = {"1000001": { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6", transaction: {transactionObj} } };


//var processingCenterId;
//var elementQueues = { "r1" : { type: "R", queue: [{ transactionId: 100001, toProcCenter = true, storeId: "s1", destinationReached: false, timeoutObj : {timeoutObj} }], limit : 10 };
//var connectionQueues = { "s1r1" : { transactionId: 100001, toProcCenter = true, storeId: "s1", timeoutObj : {timeoutObj} } }
//var timeoutObj = { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6" }

function sendTransactionToProcessCenter(transactionId, storeId) {
    moveTransaction({ transactionId: transactionId, toProcCenter: true, storeId: "s" + storeId, destinationReached: false, timeoutObj: { timeout: null, sendFunc: null, fromNode: "", toNode: "" } });
}

//function sendTransactionToStore() {

//}

function moveTransaction(transaction) {
    if (transaction.toProcCenter) {
        sendToNode(null, transaction.storeId, transaction);
    } else {
        sendToNode(null, processingCenterId, transaction);
    }
}


function sendToNode(fromNode, toNode, transaction) {

    if (fromNode !== null) {
        // Remove transaction from connection;
        var connectionId = findConnectionId(fromNode, toNode);
        elementQueues[connectionId].queue.shift();

        // Check if there is another transaction waiting for connection.
        if (elementQueues[connectionId].queue.length > 0) {
            // Still transactions waiting on connection.

            // Start the next transaction waiting for the connection.
            var nextTransaction = elementQueues[connectionId].queue.shift();
            var nextTransactionInQueueTimeout = nextTransaction.timeoutObj;
            nextTransactionInQueueTimeout.sendFunc(nextTransactionInQueueTimeout.fromNode, nextTransactionInQueueTimeout.toNode, nextTransaction);
        } else {
            // No more transactions waiting on connection.
            highlightConnection(fromNode, toNode, false);
        }
    }

    cy.$('#' + toNode).addClass('highlighted');

    elementQueues[toNode].queue.push(transaction);

    if (!queueIsAtLimit(toNode)) {

        if (!hasReachedDestination(toNode, transaction)) {

            var path = getPath(toNode, transaction);

            if (path === null) {
                // No path was found to move the transaction to destination.
                // TODO: FIX BELOW LINE IS BROKEN!!!
                elementQueues[toNode].queue.timeoutObj.timeout = null;
                return;
            }

            if (elementQueues[toNode].queue.length > 1 || graphStopped) {
                // There are other transactions in the queue before this transaction or the graph has stopped.
                transaction.timeoutObj = { timeout: null, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1] };

            } else {
                // There are no transactions in the queue before this transaction and the graph is active.

                // Start timeout to move transaction.
                var nodeTimeout = setTimeout(sendToConnection, 1000, path[0], path[1], transaction);

                // Add timeouts to transaction for pausing and resuming.
                transaction.timeoutObj = { timeout: nodeTimeout, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1] };
            }
        } else {
            // Node has reached it's destination.

            transaction.destinationReached = true;
        }
    } else {
        var path = getPath(toNode, transaction);

        if (path === null) {
            // No path was found to move the transaction to destination.
            // TODO: FIX BELOW LINE IS BROKEN!!!
            elementQueues[toNode].queue.timeoutObj.timeout = null;
            return;
        }

        transaction.timeoutObj = { timeout: null, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1] };
    }
}

function sendToConnection(fromNode, toNode, transaction) {

    var connectionId = findConnectionId(fromNode, toNode);
    // Check if there is a transaction all ready on the connection.
    if (!queueIsAtLimit(connectionId)) {
        // No transaction on the connection

        // Remove transaction from the node the transaction was just at.
        elementQueues[fromNode].queue.shift();

        // Add transaction to connection.
        elementQueues[connectionId].queue.push(transaction);

        highlightConnection(fromNode, toNode, true);

        var connectionTimeout = setTimeout(sendToNode, 1000, fromNode, toNode, transaction);

        // Add timeouts to transaction for pausing and resuming.
        transaction.timeoutObj = { timeout: connectionTimeout, sendFunc: sendToNode, fromNode: fromNode, toNode: toNode };

        if (elementQueues[fromNode].queue.length > 0) {
            // There is still transactions in the fromNode's queue, start the next one.
            var nextTransactionInQueueTimeout = elementQueues[fromNode].queue[0].timeoutObj;
            nextTransactionInQueueTimeout.sendFunc(nextTransactionInQueueTimeout.fromNode, nextTransactionInQueueTimeout.toNode, elementQueues[fromNode].queue[0]);
        } else {
            // No more transaction in the queue
            // Remove highlighting for node.
            cy.$('#' + fromNode).removeClass('highlighted');
        }

    } else {
        // Transaction already on connection.

        // Add transaction to connection queue.
        elementQueues[connectionId].queue.push(transaction);

        transaction.timeoutObj = { timeout: null, sendFunc: sendToConnection, fromNode: fromNode, toNode: toNode };
    }
}

function queueIsAtLimit(nodeId) {
    return elementQueues[nodeId].queue.length > elementQueues[nodeId].limit;
}

function hasReachedDestination(currentNode, transaction) {
    if (transaction.toProcCenter) {
        return currentNode === processingCenterId;
    } else {
        return currentNode === transaction.storeId;
    }
}

function getPath(currentNode, transaction) {
    if (transaction.toProcCenter) {
        return findPath(currentNode, processingCenterId, true);
    } else {
        return findPath(currentNode, transaction.storeId, false);
    }
}

function highlightConnection(nodeId1, nodeId2, highlight) {
    var highlightNodeId;

    for (var i = 0; i < cytoscapeEdges.length; i++) {
        if ((cytoscapeEdges[i].data.source === nodeId1 && cytoscapeEdges[i].data.target === nodeId2) ||
            (cytoscapeEdges[i].data.source === nodeId2 && cytoscapeEdges[i].data.target === nodeId1)) {
            highlightNodeId = cytoscapeEdges[i].data.source + cytoscapeEdges[i].data.target;
        }
    }
    if (highlightNodeId === undefined) {
        console.log("Could not find edge in graph for " + nodeId1 + " " + nodeId2);
        return;
    }
    if (highlight) {
        cy.$('#' + highlightNodeId).addClass('highlighted');
    } else {
        cy.$('#' + highlightNodeId).removeClass('highlighted');
    }
}

function findConnectionId(nodeId1, nodeId2) {

    if (!nodeId1 || !nodeId2) {
        console.log("One of the nodes was undefined.");
        return;
    }

    if (elementQueues[nodeId1 + nodeId2] !== undefined) {
        return nodeId1 + nodeId2;
    } else if (elementQueues[nodeId2 + nodeId1] !== undefined) {
        return nodeId2 + nodeId1;
    }

    if (connectionId === undefined) {
        console.log("Could not find connection for " + nodeId1 + " " + nodeId2);
        return;
    }
}