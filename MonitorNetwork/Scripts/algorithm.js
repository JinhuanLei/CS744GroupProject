﻿//var adj = []; //Adjacency List
//var sourceType;
//var destType;


//function startPath(source, destination) {
//    if (source === "s7") {
//        return ["s7", "r3", "r5", "r11"];
//    }

//    if (source === "r3") {
//        return ["r3", "r5", "r11"];
//    }

//    if (source === "r5") {
//        return ["r5", "r11"];
//    }

//    if (source === "s1") {
//        return ["s1", "r1", "r11"];
//    }
//}

////function startPath(s, d) {
////	var counter = 0;
////	for (var j = 0; j < connections.length + 200; j++) {
////		adj[j] = [];
////	}
////	for (var i = 0; i < connections.length; i++) {

////		if (connections[i].storeID != null) {
////			if (connections[i].destRelayID < 10) {
////				addEdge(connections[i].storeID, 10 + connections[i].destRelayID)
////			}
////			else {
////				addEdge(connections[i].storeID, 1 + connections[i].destRelayID)
////			}
////		}
////		else {
////			if (connections[i].relayID < 10) {
////				var value1 = 10 + connections[i].relayID;
////			}
////			else {
////				var value1 = 1 + connections[i].relayID;

////			}
////			if (connections[i].destRelayID < 10) {
////				var value2 = 10 + connections[i].destRelayID;
////			}
////			else {
////				var value2 = 1 + connections[i].destRelayID;
////			}

////			addEdge(value1, value2)
////		}
////	}
////	var source;
////	var dest;
////	sourceType = s.substring(0, 1);
////	destType = d.substring(0, 1);
////	if (sourceType == "s") {
////		source = s.substring(1, s.length);
////	}
////	else if (sourceType == "r") {
////		source = Math.round(s.substring(1, s.length)) + 100;
////	}

////	if (d.substring(0, 1) == "s") {
////		dest = d.substring(1, d.length);
////	}
////	else if (d.substring(0, 1) == "r") {
////		dest = Math.round(d.substring(1, d.length)) + 100;
////	}

////	list = isReachable(source, dest);

////	document.write(list);

////	return list;
////}

//////Function to add an edge into the graph
////function addEdge(v, w) {
////	adj[v][w] = w;

////	adj[w][v] = v;
////}

//////prints BFS traversal from a given source s
////function isReachable(s, d) {
////	var temp = [];
////	var visited = [];
////	var queue = [];
////	var path = [];

////	// Mark the current node as visited and enqueue it
////	visited[s] = true;
////	queue.push(s);
////	if (sourceType == "s") {
////		path.push("s" + s);
////	}
////	else if (sourceType == "r") {
////		path.push("r" + (s - 100));
////	}

////	while (queue.length > 0) {
////		s = queue.pop();
////		if (queue.length > 0 && destType == "s") {
////			path.pop();
////		}

////		var n;

////		for (var i = 0; i < adj[s].length; i++) {
////			n = adj[s][i];
////			if (d == n) {
////				console.log("FINISHED!");
				
////				if (destType == "s") {
////					path.push("s" + n);
////				}
////				else if (destType == "r") {
////					path.push("r" + (n - 100));
////				}

////				for (var k = 0; k < path.length; k++) {
////					if (path[k] > 100) {
////						path[k] = "r" + (path[k] - 100);
////					}
////				}
				
////				return path;
////			}

////			if (!visited[n]) {
////				visited[n] = true;
////				if (n) {
////					queue.push(n);
////				}
////				if (n > 100) {
////					path.push(n);
////				}
////			}
////		}
////	}

////	return false;
////}