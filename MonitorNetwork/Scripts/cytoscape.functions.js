

function cytoscapeClickEdge(evt) {
    //alert(evt.target.id());
    var edgeId = evt.target.id();
    var start = "";
    var dest = "";
    var arr = edgeId.split("r");
    var connection;
    if (edgeId[0] === "r") {
        start = arr[1];
        dest = arr[2];
        connection = connections.find(function (connection) {
            return connection.relayID == start && connection.destRelayID == dest;
        });


    } else {
        start = arr[0].substring(1);
        dest = arr[1];

        connection = connections.find(function (connection) {
            return connection.storeID == start && connection.destRelayID == dest;
        });
    }
    connection.isActive = !connection.isActive;

    toggleElementState(edgeId, "edge", connection.isActive);

    $.ajax({
        type: "GET",
        url: '/Home/SetConnectionActive',
        data: {
            connectionId: connection.connID,
            isActive: connection.isActive
        }
    });
}

function cytoscapeClickNode(evt) {
    var nodeId = evt.target.id();

    var nodeIdNumber = (Number)(nodeId.substring(1));

    var relay = relays.find(function (relay) {
        return relay.relayID === nodeIdNumber;
    });

    if (relay != null && !relay.isProcessingCenter) {

        relay.isActive = !relay.isActive;

        toggleElementState(nodeId, "node", relay.isActive);

        $.ajax({
            type: "GET",
            url: '/Home/SetRelayActive',
            data: {
                relayId: relay.relayID,
                isActive: relay.isActive
            }
        });
    }
}

function toggleElementState(elementId, type, isActive) {
    if (isActive) {
        cy.$("#" + elementId).classes(type);
    }
    else {
        cy.$("#" + elementId).classes('newStyle');
    }
}


function cytoscapeToolTip() {
    var nodeId = this.data('id');
    var label = this.data('label');
    var transactionIds = [];

    var numberOfTransactionsToShow = elementQueues[nodeId].showLimit;

    for (var i = 0; i < numberOfTransactionsToShow; i++) {
        transactionIds.push(elementQueues[nodeId].queue[i].transactionId);
    }

    var queueStr = transactionIds.join(", ");
    if (nodeId[0] == "r") {
        return '192.168.' + this.data('label') + '\n Limit: ' + elementQueues[nodeId].limit + '\nQueue:' + queueStr
    } else {
        var merchantName;
        stores.forEach(function (t, number, ts) {
            //console.log(t.storeID + "   " + nodeId.substring(1));
            //console.log(t.storeID == nodeId.substring(1));
            if (t.storeID == nodeId.substring(1)) {

                merchantName = t.merchantName;
            }
        });
        return merchantName + ':192.168.' + label + '\n Queue:' + queueStr
    }

}

//function getStoreIpAdress(storeID) {
//    console.log(storeID);
//    var nodeid = storeID;
//    var id = (Number)(nodeid.substring(1));
//    //alert(evt.target.id() + "  " + id);
//    for (var x = 0; x < stores.length; x++) {
//        //  alert(relays[x].relayID + "  " + id);
//        if (stores[x].storeID == id) {

//            return stores[x].storeIP;
//        }
//    }
//}
//function getRelayIpAdress(relayID) {
//    console.log(relayID);
//    var nodeid = relayID;
//    var id = (Number)(nodeid.substring(1));
//    //alert(evt.target.id() + "  " + id);
//    for (var x = 0; x < relays.length; x++) {
//        //  alert(relays[x].relayID + "  " + id);
//        if (relays[x].relayID == id) {
//            return relays[x].relayIP;
//        }
//    }
//}