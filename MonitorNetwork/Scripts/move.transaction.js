
//var processingCenterId;
//var elementQueues = { "r1" : { type: "R", queue: [{ transactionId: 100001, toProcCenter = true, storeId: "s1", destinationReached: false, timeoutObj : {timeoutObj} }], limit : 10 };
//var connectionQueues = { "s1r1" : { transactionId: 100001, toProcCenter = true, storeId: "s1", timeoutObj : {timeoutObj} } }
//var timeoutObj = { timeout: func, sendFunc: func, fromNode: "r1", toNode: "s6" }


var MILLI_SECOND_MOVEMENT_SPEED = 1000;

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
