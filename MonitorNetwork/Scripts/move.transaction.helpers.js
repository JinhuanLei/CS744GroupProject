

// Sends the transaction to its defined next element.
function sendTransactionToElement(transaction) {
    var transactionTimeout = transaction.timeoutObj;
    transactionTimeout.sendFunc(transactionTimeout.fromNode, transactionTimeout.toNode, transaction);
}

function queueIsAtLimit(nodeId) {
    return getQueueLength(nodeId) >= elementQueues[nodeId].limit;
}

function getQueueLength(elementId) {
    return elementQueues[elementId].queue.length;
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

function transactionExists(transactionId, elementArray) {
    for (var i = 0; i < elementArray.length; i++) {
        if (elementArray[i].transactionId === transactionId) {
            return true;
        }
    }

    return false;
}

function addCSSClassToConnection(nodeId1, nodeId2, cssClass) {
    var connectionId = getGraphConnectionId(nodeId1, nodeId2);

    if (connectionId === undefined) {
        console.log("Could not find edge in graph for " + nodeId1 + " " + nodeId2);
        return;
    }

    cy.$('#' + connectionId).addClass(cssClass);
}

function removeCSSClassToConnection(nodeId1, nodeId2, cssClass) {
    var connectionId = getGraphConnectionId(nodeId1, nodeId2);

    if (connectionId === undefined) {
        console.log("Could not find edge in graph for " + nodeId1 + " " + nodeId2);
        return;
    }

    cy.$('#' + connectionId).removeClass(cssClass);
}

function getGraphConnectionId(nodeId1, nodeId2) {
    for (var i = 0; i < cytoscapeEdges.length; i++) {
        if ((cytoscapeEdges[i].data.source === nodeId1 && cytoscapeEdges[i].data.target === nodeId2) ||
            (cytoscapeEdges[i].data.source === nodeId2 && cytoscapeEdges[i].data.target === nodeId1)) {
            return cytoscapeEdges[i].data.source + cytoscapeEdges[i].data.target;
        }
    }

    return undefined;
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