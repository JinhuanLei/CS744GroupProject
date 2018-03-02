var adj = []; //Adjacency List


function startPath(s, d, toProc) {
	var counter = 0;
	for (var j = 0; j < obj.connections.length + 200; j++) {
		adj[j] = [];
	}
	for (var i = 0; i < obj.connections.length; i++) {


		if (obj.connections[i].active === "True") {
			console.log(i + " " + obj.connections[i].active);
			if (obj.connections[i].storeID != "NULL") {
				if (obj.connections[i].destRelayID < 10) {
					addEdge(obj.connections[i].storeID, 10 + obj.connections[i].destRelayID)
				}
				else {
					addEdge(obj.connections[i].storeID, 1 + obj.connections[i].destRelayID)

				}
			}
			else {
				if (obj.connections[i].relayID < 10) {
					var value1 = 10 + obj.connections[i].relayID;
				}
				else {
					var value1 = 1 + obj.connections[i].relayID;

				}
				if (obj.connections[i].destRelayID < 10) {
					var value2 = 10 + obj.connections[i].destRelayID;
				}
				else {
					var value2 = 1 + obj.connections[i].destRelayID;
				}

				addEdge(value1, value2)
			}
		}

	}
	if (toProc) {
		console.log(adj);
		if (d < 10) {

			list = isReachable(s, 100 + d + "", toProc);
		}
		else {
			console.log("d = " + (100 + d));
			list = isReachable(s, 100 + d + "", toProc);

		}
	}
	else {
		if (s < 10) {
			list = isReachable(s + 100 + "", d, toProc);
		}
		else {
			list = isReachable(s + 1 + "", d, toProc);
		}
	}
	document.write(list);

	return list;
}

//Function to add an edge into the graph
function addEdge(v, w) {
	adj[v][w] = w;

	adj[w][v] = v;

	//adj[w].fill([v]);   

}

//prints BFS traversal from a given source s
function isReachable(s, d, toProc) {
	var temp = [];
	var visited = [];
	var queue = [];
	var path = [];
	var popped = false;

	// Mark the current node as visited and enqueue it
	visited[s] = true;
	queue.push(s);
	if (toProc) {
		path.push("s" + s);
	}
	else {
		path.push("r" + (s - 100));
	}

	while (queue.length > 0) {
		console.log("queue before popping s: " + queue);
		s = queue.pop();


		//path.pop();
		console.log("s " + s);
		console.log("queue after popping s: " + queue);

		var n;

		for (var i = 0; i < adj[s].length; i++) {
			n = adj[s][i];
			console.log("n = " + n + " and d = " + d);
			if (d == n) {
				console.log("FINISHED!");
				console.log("QUEUE: " + queue)
				console.log(" n " + n);
				if (!toProc) {
					path.push("s" + n);
				}
				else {
					path.push("r" + (n - 100));
				}
				console.log(path);

				for (var k = 0; k < path.length; k++) {
					if (path[k] > 100) {
						path[k] = "r" + (path[k] - 100);
						console.log("Path[k] " + path[k]);
					}
				}


				console.log(path);
				return path;
			}

			if (!visited[n]) {
				visited[n] = true;
				queue.push(n);
				console.log(" n " + n);
				if (toProc && n > 100) {
					path.push(n);
					console.log(path);
				}
				else if (!toProc && n) {
					path.push(n);
					console.log(path);
				}
			}
		}
	}

	return false;
}