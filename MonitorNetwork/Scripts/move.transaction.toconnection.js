//var processingCenterId;
//var elementQueues = { "r1" : { type: "R", queue: [{ transactionId: 100001, toProcCenter = true, storeId: "s1", destinationReached: false, timeoutObj : {timeoutObj} }], limit : 10 };
//var connectionQueues = { "s1r1" : { transactionId: 100001, toProcCenter = true, storeId: "s1", timeoutObj : {timeoutObj} } }
//var timeoutObj = { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6" }


function moveTransactionFromNodeToConnection(fromNode, toNode, connectionId, transaction) {
    // Remove transaction from the node the transaction was just at.
    elementQueues[fromNode].queue.shift();

    highlightConnection(fromNode, toNode, true);

    var connectionTimeout = setTimeout(sendToNode, MILLI_SECOND_MOVEMENT_SPEED, fromNode, toNode, transaction);

    // Add transaction to connection.
    addTransactionToElementQueue(connectionId, transaction, { timeout: connectionTimeout, sendFunc: sendToNode, fromNode: fromNode, toNode: toNode });
}

function sendToConnection(fromNode, toNode, transaction) {

    // The node queue the transaction needs to go to is full.
    if (queueIsPassedLimit(toNode)) {
        // When the transaction head of the full queue shifts off, if this
        // transaction is waiting on the full queue, and next in line
        // it will start this transaction from it's fromNode location.
        // allowing it to move off the fromNode stack on to the toNode.
        addTransactionToElementQueue(connectionId, transaction, { timeout: null, sendFunc: sendToConnection, fromNode: fromNode, toNode: toNode });
        return;
    }

    var connectionId = findConnectionId(fromNode, toNode);
    // Check if there is a transaction all ready on the connection.
    if (!queueIsPassedLimit(connectionId)) {
        // No transaction on the connection

        // Get transaction that is in the queue but are out of limits.
        var queueLimitTransacion = getOutOfQueueLimitTransaction(elementId);

        moveTransactionFromNodeToConnection(fromNode, toNode, connectionId, transaction);

        if (queueLimitTransaction != null) {

        }

        if (getQueueLength(fromNode) > 0) {
            // There is still transactions in the fromNode's queue, start the next one.
            sendTransactionToElement(elementQueues[fromNode].queue[0]);

        } else {
            // No more transaction in the queue
            // Remove highlighting for node.
            cy.$('#' + fromNode).removeClass('highlighted');
        }

    } else {
        // Transaction already on connection. Wait for transaction that
        // is using the line to finish, and start this connection.

        // Add the transaction to the connection queue.
        addTransactionToElementQueue(connectionId, transaction, { timeout: null, sendFunc: sendToConnection, fromNode: fromNode, toNode: toNode });
    }
}