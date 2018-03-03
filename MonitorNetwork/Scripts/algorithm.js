var adj = []; //Adjacency List


function startPath(s, d, toProc) {
    return ["s7", "r3", "r5", "r11"];
}

//function startPath(s, d, toProc) {
//	var counter = 0;
//	for (var j = 0; j < connections.length + 200; j++) {
//		adj[j] = [];
//	}
//	for (var i = 0; i < connections.length; i++) {


//		if (connections[i].active) {
//			console.log(i + " " + connections[i].active);
//			if (connections[i].storeID != "NULL") {
//				if (connections[i].destRelayID < 10) {
//					addEdge(connections[i].storeID, 10 + connections[i].destRelayID)
//				}
//				else {
//					addEdge(connections[i].storeID, 1 + connections[i].destRelayID)

//				}
//			}
//			else {
//				if (connections[i].relayID < 10) {
//					var value1 = 10 + connections[i].relayID;
//				}
//				else {
//					var value1 = 1 + connections[i].relayID;

//				}
//				if (connections[i].destRelayID < 10) {
//					var value2 = 10 + connections[i].destRelayID;
//				}
//				else {
//					var value2 = 1 + connections[i].destRelayID;
//				}

//				addEdge(value1, value2)
//			}
//		}

//	}
//	if (toProc) {
//		console.log(adj);
//		if (d < 10) {

//			list = isReachable(s, 100 + d + "", toProc);
//		}
//		else {
//			console.log("d = " + (100 + d));
//			list = isReachable(s, 100 + d + "", toProc);

//		}
//	}
//	else {
//		if (s < 10) {
//			list = isReachable(s + 100 + "", d, toProc);
//		}
//		else {
//			list = isReachable(s + 1 + "", d, toProc);
//		}
//	}
//	document.write(list);

//	return list;
//}

////Function to add an edge into the graph
//function addEdge(v, w) {
//	adj[v][w] = w;

//	adj[w][v] = v;

//	//adj[w].fill([v]);   

//}

////prints BFS traversal from a given source s
//function isReachable(s, d, toProc) {
//	var temp = [];
//	var visited = [];
//	var queue = [];
//	var path = [];
//	var popped = false;

//	// Mark the current node as visited and enqueue it
//	visited[s] = true;
//	queue.push(s);
//	if (toProc) {
//		path.push("s" + s);
//	}
//	else {
//		path.push("r" + (s - 100));
//	}

//	while (queue.length > 0) {
//		console.log("queue before popping s: " + queue);
//		s = queue.pop();


//		//path.pop();
//		console.log("s " + s);
//		console.log("queue after popping s: " + queue);

//		var n;

//		for (var i = 0; i < adj[s].length; i++) {
//			n = adj[s][i];
//			console.log("n = " + n + " and d = " + d);
//			if (d == n) {
//				console.log("FINISHED!");
//				console.log("QUEUE: " + queue)
//				console.log(" n " + n);
//				if (!toProc) {
//					path.push("s" + n);
//				}
//				else {
//					path.push("r" + (n - 100));
//				}
//				console.log(path);

//				for (var k = 0; k < path.length; k++) {
//					if (path[k] > 100) {
//						path[k] = "r" + (path[k] - 100);
//						console.log("Path[k] " + path[k]);
//					}
//				}


//				console.log(path);
//				return path;
//			}

//			if (!visited[n]) {
//				visited[n] = true;
//				queue.push(n);
//				console.log(" n " + n);
//				if (toProc && n > 100) {
//					path.push(n);
//					console.log(path);
//				}
//				else if (!toProc && n) {
//					path.push(n);
//					console.log(path);
//				}
//			}
//		}
//	}

//	return false;
//}