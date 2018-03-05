/// Source: https://stackoverflow.com/questions/32527026/shortest-path-in-javascript

function Graph() {
    var neighbors = this.neighbors = {}; // Key = vertex, value = array of neighbors.

    this.addBiEdge = function (u, v) {
        if (neighbors[u] === undefined) {  // Add the edge u -> v.
            neighbors[u] = [];
        }
        neighbors[u].push(v);
        if (neighbors[v] === undefined) {  // Also add the edge v -> u in order
            neighbors[v] = [];             // to implement an undirected graph.
        }                                  // For a directed graph, delete
        neighbors[v].push(u);              // these four lines.
    };

    this.addUniEdge = function (u, v) {
        if (neighbors[u] === undefined) {  // Add the edge u -> v.
            neighbors[u] = [];
        }
        neighbors[u].push(v);
    }

    this.addNode = function (u) {
        if (neighbors[u] === undefined) {
            neighbors[u] = [];
        }
    }

    return this;
}

function findPath(source, target, toProcCenter) {
    var graph = constructGraph(toProcCenter);
    if (source === target) {   // Delete these four lines if
        console.log(source);  // you want to look for a cycle
        return;               // when the source is equal to
    }                         // the target.
    var queue = [source],
        visited = { source: true },
        predecessor = {},
        tail = 0;
    while (tail < queue.length) {
        var u = queue[tail++],  // Pop a vertex off the queue.
            neighbors = graph.neighbors[u];
        for (var i = 0; i < neighbors.length; ++i) {
            var v = neighbors[i];
            if (visited[v]) {
                continue;
            }
            visited[v] = true;
            if (v === target) {   // Check if the path is complete.
                var path = [v];   // If so, backtrack through the path.
                while (u !== source) {
                    path.push(u);
                    u = predecessor[u];
                }
                path.push(u);
                path.reverse();
                //console.log(path.join(', '));
                return path;
            }
            predecessor[v] = u;
            queue.push(v);
        }
    }
    console.log('there is no path from ' + source + ' to ' + target);
    return null;
}

function constructGraph(toProcCenter) {
    var graph = new Graph();


    relays.forEach(function (relay) {
        graph.addNode("r" + relay.relayID);
    });

    stores.forEach(function (store) {
        graph.addNode("s" + store.storeID);
    });

    connections.forEach(function (connection) {
        if (connection.active) {
            var source;

            if (isRelayActive(connection.destRelayID)) {
                if (connection.relayID !== null && isRelayActive(connection.relayID)) {
                    graph.addBiEdge("r" + connection.relayID, "r" + connection.destRelayID);
                } else {
                    if (toProcCenter) {
                        graph.addUniEdge("s" + connection.storeID, "r" + connection.destRelayID);
                    } else {
                        graph.addUniEdge("r" + connection.storeID, "s" + connection.destRelayID);
                    }
                }
            }
        }
    });

    return graph;
}

function isRelayActive(relayId) {
    return relays.some(function (relay) {
        if (relay.relayID === relayId && relay.status) {
            return true;
        }
        return false;
    });
}