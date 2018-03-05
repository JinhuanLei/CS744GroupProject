
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
        highlightConnection(fromNode, toNode, false);
    }

    cy.$('#' + toNode).addClass('highlighted');

    nodeQueues[toNode].push(transaction);

    if (!hasReachedDestination(toNode, transaction)) {

        var path = getPath(toNode, transaction);

        if (path === null) {
            delete timeouts[transaction.transactionId];
            return;
        }

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

    highlightConnection(fromNode, toNode, true);

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
        return findPath(currentNode, processingCenterId, true);
    } else {
        return findPath(currentNode, transaction.storeId, false);
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