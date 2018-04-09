//var processingCenterId;
//var nodeQueues = { "r1" : [{ transactionId: 100001, toProcCenter = true, storeId: "s1", destinationReached: false, timeoutObj : {timeoutObj} }] };
//var connectionQueues = { "s1r1" : { transactionId: 100001, toProcCenter = true, storeId: "s1", timeoutObj : {timeoutObj} } }
//var timeoutObj = { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6" }

var MILLI_SECOND_MOVEMENT_SPEED = 1500;

function sendTransactionToProcessCenter(transactionId, storeId) {
    moveTransaction({ transactionId: transactionId, toProcCenter: true, storeId: "s" + storeId, destinationReached: false, timeoutObj: { timeout: null, sendFunc: null, fromNode: "", toNode: "" } });
}

function sendTransactionToStore(transactionId, storeId) {
	moveTransaction({ transactionId: transactionId, toProcCenter: false, storeId: "s" + storeId, destinationReached: false, timeoutObj: { timeout: null, sendFunc: null, fromNode: "", toNode: "" } });
}

function moveTransaction(transaction) {
    if (transaction.toProcCenter) {
        sendToNode(null, transaction.storeId, transaction);
    } else {
        sendToNode(null, processingCenterId, transaction);
    }
}

function sendToNode(fromNode, toNode, transaction) {

    if (fromNode !== null) {

        if (queueIsAtLimit(toNode)) {
            droppedTransaction(fromNode, toNode, transaction);
            return;
        }

        processFromConnection(fromNode, toNode);
    }

    cy.$('#' + toNode).addClass('highlighted');

    if (!transactionExists(transaction.transactionId, elementQueues[toNode].queue)) {
        elementQueues[toNode].queue.push(transaction);
    }

    if (hasReachedDestination(toNode, transaction)) {
        // Transaction has reached it's destination.

		transaction.destinationReached = true;
        if (transaction.toProcCenter) {
            reachedProcessingCenter(transaction.transactionId);
        }
        else
        {
            setTimeout(function () {
                if (getQueueLength(toNode) <= 1) {
                    // No more transaction in the queue
                    // Remove highlighting for node.
                    cy.$('#' + toNode).removeClass('highlighted');
                }
            }, MILLI_SECOND_MOVEMENT_SPEED);

            /// TODO: Maddie - Needs another ajax method call below this comment 
            /// to decrypt transaaction and show transaction details in the table 
            /// below the graph.
        }

        return;
    }

    var path = getPath(toNode, transaction);

    if (path === null) {
        // No path was found to move the transaction to destination.
        elementQueues[toNode].queue[0].timeoutObj.timeout = null;
        return;
    }

    if (getQueueLength(toNode) > 1 || graphStopped) {
        // There are other transactions in the queue before this transaction or the graph has stopped.
        transaction.timeoutObj = { timeout: null, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1] };

    } else {
        // There are no transactions in the queue before this transaction and the graph is active.

        // Start timeout to move transaction.
        var nodeTimeout = setTimeout(sendToConnection, MILLI_SECOND_MOVEMENT_SPEED, path[0], path[1], transaction);

        // Add timeouts to transaction for pausing and resuming.
        transaction.timeoutObj = { timeout: nodeTimeout, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1] };
    }
}

function sendToConnection(fromNode, toNode, transaction) {

    var connectionId = findConnectionId(fromNode, toNode);
    // Check if there is a transaction all ready on the connection.
    if (!queueIsAtLimit(connectionId)) {
        // No transaction on the connection

        // Remove transaction from the node the transaction was just at.
        elementQueues[fromNode].queue.shift();

        if (getQueueLength(fromNode) <= 0) {
            // No more transaction in the queue
            // Remove highlighting for node.
            cy.$('#' + fromNode).removeClass('highlighted');
        }

        // Add transaction to connection, if it hasn't been added.
        elementQueues[connectionId].queue.push(transaction);

        addCSSClassToConnection(fromNode, toNode, "highlighted");

        var connectionTimeout = setTimeout(sendToNode, MILLI_SECOND_MOVEMENT_SPEED, fromNode, toNode, transaction);

        // Add timeouts to transaction for pausing and resuming.
        transaction.timeoutObj = { timeout: connectionTimeout, sendFunc: sendToNode, fromNode: fromNode, toNode: toNode };

        if (getQueueLength(fromNode) > 0) {
            // There is still transactions in the fromNode's queue, start the next one.
            sendTransactionToElement(elementQueues[fromNode].queue[0]);
        }

    } else {
        // Transaction already on connection.

        // Add transaction to connection queue.
        if (!transactionExists(transaction.transactionId, elementQueues[connectionId].queue)) {
            elementQueues[connectionId].queue.push(transaction);
        }

        transaction.timeoutObj = { timeout: null, sendFunc: sendToConnection, fromNode: fromNode, toNode: toNode };
    }
}

function processFromConnection(fromNode, toNode) {
    // Remove transaction from connection;
    var connectionId = findConnectionId(fromNode, toNode);
    elementQueues[connectionId].queue.shift();

    // Check if there is another transaction waiting for connection.
    if (getQueueLength(connectionId) > 0) {
        // Still transactions waiting on connection.

        // Start the next transaction waiting for the connection.
        var nextTransaction = elementQueues[connectionId].queue.shift();
        sendTransactionToElement(nextTransaction);
    } else {
        // No more transactions waiting on connection.
        removeCSSClassToConnection(fromNode, toNode, "highlighted");
    }
}

function droppedTransaction(fromNode, toNode, transaction) {
    removeCSSClassToConnection(fromNode, toNode, "highlighted");

    addCSSClassToConnection(fromNode, toNode, "dropped");

    var connectionTimeout = setTimeout(removeDroppedTransaction, MILLI_SECOND_MOVEMENT_SPEED, fromNode, toNode, transaction);

    // Add timeouts to transaction for pausing and resuming.
    transaction.timeoutObj = { timeout: connectionTimeout, sendFunc: removeDroppedTransaction, fromNode: fromNode, toNode: toNode };
}

function removeDroppedTransaction(fromNode, toNode, transaction) {
    removeCSSClassToConnection(fromNode, toNode, "dropped");
    processFromConnection(fromNode, toNode);
}