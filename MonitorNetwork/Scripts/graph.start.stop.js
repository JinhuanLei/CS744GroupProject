var graphStopped = false;


function toggleFlow() {
    if (graphStopped) {
        resumeFlow();
        $("#flowBtn").html("Stop");
    } else {
        stopFlow();
        $("#flowBtn").html("Resume");
    }
}

function stopFlow() {

    // Stop new transactions from moving on the graph.
    graphStopped = true;

    // Stop transactions from moving on the graph.
    Object.keys(elementQueues).forEach(function (elementId) {
        if (elementQueues[elementId].queue.length > 0) {
            clearTimeout(elementQueues[elementId].queue[0].timeoutObj.timeout);
        }
    });
}

function resumeFlow() {

    // Allow new transactions to move on the graph.
    graphStopped = false;

    // Start transactions to move on graph.
    var sendTransactions = []
    Object.keys(elementQueues).forEach(function (elementId) {
        if (elementQueues[elementId].queue.length > 0 && !elementQueues[elementId].queue[0].destinationReached) {
            sendTransactions.push(elementQueues[elementId].queue[0]);
        }
    });

    sendTransactions.forEach(function (sendTransaction) {
        sendTransaction.timeoutObj.sendFunc(sendTransaction.timeoutObj.fromNode, sendTransaction.timeoutObj.toNode, sendTransaction);
    })
}