var graphStopped = false;

function stopFlow() {
    // Enable resume button, disable stop button.
    $("#resumeFlowBtn").removeAttr('disabled');
    $("#stopFlowBtn").attr('disabled', 'disabled');

    // Stop new transactions from moving on the graph.
    graphStopped = true;

    // Stop transactions from moving on the graph.
    Object.keys(elementQueues).forEach(function (elementId) {
        if (elementQueues[elementId].length > 0) {
            clearTimeout(elementQueues[elementId][0].timeoutObj.timeout);
        }
    });
}

function resumeFlow() {
    // Enable stop button, disable resume button.
    $("#resumeFlowBtn").attr('disabled', 'disabled');
    $("#stopFlowBtn").removeAttr('disabled');

    // Allow new transactions to move on the graph.
    graphStopped = false;

    // Start transactions to move on graph.
    var sendTransactions = []
    Object.keys(elementQueues).forEach(function (elementId) {
        if (elementQueues[elementId].length > 0 && !elementQueues[elementId][0].destinationReached) {
            sendTransactions.push(elementQueues[elementId][0]);
        }
    });

    sendTransactions.forEach(function (sendTransaction) {
        sendTransaction.timeoutObj.sendFunc(sendTransaction.timeoutObj.fromNode, sendTransaction.timeoutObj.toNode, sendTransaction);
    })
}