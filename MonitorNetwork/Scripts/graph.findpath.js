var connectionPath = [];

function startPath(s1) {
    storeToRelay(s1);

    return connectionPath;
}

function storeToRelay(s1) {
    var next = connections[0];

    if (next.storeID !== s1 || !next.active) {
        for (var i = 0; i < connections.length; i++) {
            if (connections[i].storeID === s1 && connections[i].active) {
                next = connections[i]
            }
        }
    }
    if (next.storeID === s1) {
        connectionPath.push("S" + s1);

    }
    else {
        console.log("Store does not exist!");

        return 0;
    }
    for (var i = 0; i < connections.length; i++) {
        if (connections[i].weight <= next.weight && connections[i].storeID === s1) {
            next = connections[i];

        }

    }
    connectionPath.push("R" + next.destRelayID);
    relayToRelay(next.destRelayID);
}

function relayToRelay(r1) {
    var next = connections[0];

    if (next.relayID !== r1 || !next.active) {
        for (var i = 0; i < connections.length; i++) {
            if (connections[i].relayID === r1 && connections[i].active) {
                next = connections[i]
            }
        }
    }
    for (var i = 0; i < connections.length; i++) {
        if (connections[i].weight <= next.weight && connections[i].relayID === r1) {
            next = connections[i];

        }


    }
    connectionPath.push("R" + next.destRelayID);
    //Will need to change this to our real processing center ID
    if (next.destRelayID !== 11) {
        relayToRelay(next.destRelayID);
    }
}