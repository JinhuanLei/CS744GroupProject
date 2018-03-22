//var processingCenterId;
//var elementQueues = { "r1" : { type: "R", queue: [{ transactionId: 100001, toProcCenter = true, storeId: "s1", destinationReached: false, timeoutObj : {timeoutObj} }], limit : 10 };
//var connectionQueues = { "s1r1" : { transactionId: 100001, toProcCenter = true, storeId: "s1", timeoutObj : {timeoutObj} } }
//var timeoutObj = { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6" }


// Queue of the node that the transaction came from needs to be processed.
function processPastNodeQueue(fromNode, toNode, transaction) {

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
        highlightConnection(fromNode, toNode, false);
    }
}

function processTransactionAtDestination(transaction) {
    // Node has reached it's destination.
    transaction.destinationReached = true;
    transaction.timeoutObj.timeout = null;

    // TODO: Check if transaction is at processing center
    // and needs to be processed, or is returning to store
    // to be processed. Remember to remove transaction from store
    // stack when its been processed.
}

function sendToNode(fromNode, toNode, transaction) {

    elementQueues[toNode].queue.push(transaction);

    // Check to see if the queue of the node that the transaction came from
    // needs to be processed.
    if (fromNode !== null) {
        processPastNodeQueue(fromNode, toNode, transaction);
    }

    if (hasReachedDestination(toNode, transaction)) {
        processTransactionAtDestination(transaction);
        return;
    }

    cy.$('#' + toNode).addClass('highlighted');

    var path = getPath(toNode, transaction);

    if (path === null) {
        // No path was found to move the transaction to destination.
        transaction.timeoutObj.timeout = null;
        return;
    }

    elementQueues[path[1]].queue.push(transaction);

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