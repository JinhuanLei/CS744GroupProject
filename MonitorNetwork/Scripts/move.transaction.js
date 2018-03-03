
//var processingCenterId;
//var nodeQueues = { "r1" : [{ transactionId: 100001, toProcCenter = true, storeId: "s1" }] };

function sendTransactionToProcessCenter(transactionId, storeId) {
    moveTransaction({ transactionId: transactionId, toProcCenter: true, storeId: "s" + storeId });
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

//var timeouts = {"1000001": { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6", transaction: {transactionObj} } };

var timeouts = {};

function sendToNode(fromNode, toNode, transaction) {

    //temppath = path1;

    if (fromNode !== null) {
        cy.$('#' + fromNode + toNode).removeClass('highlighted');
    }

    cy.$('#' + toNode).addClass('highlighted');

    nodeQueues[toNode].push(transaction);

    if (!hasReachedDestination(toNode, transaction)) {

        var path = getPath(toNode, transaction);

        var nodeTimeout = setTimeout(sendToConnection, 1000, path[0], path[1], transaction);

        // Add timeouts to timeouts object for pausing and resuming.
        timeouts[transaction.transactionId] = { timeout: nodeTimeout, sendFunc: sendToConnection, fromNode: path[0], toNode: path[1], transaction: transaction };
    } else {
        // Node has reached it's destination.

        // Have to remove timeouts from timeout object.
        delete timeouts[transaction.transactionId];
    }
}

function sendToConnection(fromNode, toNode, transaction) {

    cy.$('#' + fromNode).removeClass('highlighted');

    cy.$('#' + fromNode + toNode).addClass('highlighted');

    nodeQueues[fromNode].shift();

    var connectionTimeout = setTimeout(sendToNode, 1000, fromNode, toNode, transaction);

    // Add timeouts to timeouts object for pausing and resuming.
    timeouts[transaction.transactionId] = { timeout: connectionTimeout, sendFunc: sendToNode, fromNode: fromNode, toNode: toNode, transaction: transaction };
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
        return findPath(currentNode, processingCenterId);
    } else {
        return findPath(currentNode, transaction.storeId);
    }
}

function stopFlow() {
    Object.keys(timeouts).forEach(function (transactionId) {
        clearTimeout(timeouts[transactionId].timeout);
    });
}

function startFlow() {
    Object.keys(timeouts).forEach(function (transactionId) {
        var timeoutData = timeouts[transactionId];
        timeoutData.sendFunc(timeoutData.fromNode, timeoutData.toNode, timeoutData.transaction);
    });
}