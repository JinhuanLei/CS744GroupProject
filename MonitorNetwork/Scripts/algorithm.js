var adj = []; //Adjacency List
var sourceType;
var destType;
var path = [];
function startPath(s, d) {
	var counter = 0;
	for (var j = 0; j < 200; j++) {
		adj[j] = [];
	}
	path = [];
	for (var i = 0; i < connections.length; i++) {
		if (connections[i].active) {
			if (connections[i].storeID != null) {
				addEdge(connections[i].storeID, 100 + Math.round(connections[i].destRelayID))
			}
			else {
				var value1 = 100 + Math.round(connections[i].relayID);
				var value2 = 100 + Math.round(connections[i].destRelayID);

				addEdge(value1, value2)
			}
		}
	}
	var source;
	var dest;
	sourceType = s.substring(0, 1);
	destType = d.substring(0, 1);
	if (sourceType == "s") {
		source = s.substring(1, s.length);
	}
	else if (sourceType == "r") {
		source = Math.round(s.substring(1, s.length)) + 100;
	}

	if (d.substring(0, 1) == "s") {
		dest = d.substring(1, d.length);
	}
	else if (d.substring(0, 1) == "r") {
		dest = Math.round(d.substring(1, d.length)) + 100;
	}
	if (destType == "s") {
		checkForPath(dest, source);
		return path.reverse();
	}

	checkForPath(source, dest);



	//document.write(list);
	console.log(path);
	return path;
}

//Function to add an edge into the graph
function addEdge(v, w) {
	adj[v][w] = w;

	adj[w][v] = v;
}

//prints BFS traversal from a given source s
function isReachable(s, d, visited, parent) {
	var temp = [];
	var queue = [];


	//console.log(visited);


	if (s == d) {
		return true;
	}

	visited[s] = true;

	for (var i = 0; i < adj[s].length; i++) {
		if (!visited[i]) {
			parent[i] = s;

			if (isReachable(i, d, visited, parent)) {
				return true;
			}
		}

	}

	return false;
}

function checkForPath(s, d) {

	visited = [];
	parent = [];

	for (var i = 0; i < 200; i++) {
		visited[i] = "";
		parent[i] = "";
	}

	if (isReachable(s, d, visited, parent)) {
		console.log("Path: ");
		printPath(s, d, parent);
		return path;
	}
	else {
		return false;
	}
}

function printPath(s, d, parent) {
	if (d == s) {
		console.log("s" + s + " ");
		path.push("s" + s);

		return;
	}

	printPath(s, parent[d], parent);
	//console.log(parent);
	if (d > 100) {
		console.log("r" + (Math.round(d) - 100) + "");
		path.push("r" + (Math.round(d) - 100) + "")
	}
	else if (sourceType == "s") {
	}

}