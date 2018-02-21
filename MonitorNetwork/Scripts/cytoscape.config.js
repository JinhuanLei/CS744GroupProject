var cy = cytoscape({
    container: document.getElementById('cy'),

    boxSelectionEnabled: false,
    autounselectify: true,

    style: cytoscape.stylesheet()
        .selector('node')
        .css({
            // 'content': 'data(id)',
            'label': 'data(label)'
        })
        .selector("node[id>='S1']")
        .css({
            'label': 'data(label)',
            'shape': 'triangle',
        })
        .selector("node[id<'S1']")
        .css({
            'label': 'data(label)',
            'background-color': '#000000',
        })
        .selector("node[id='R11']")
        .css({
            'label': 'data(label)',
            'shape': 'rectangle',
            'background-color': '#FFF000'
        })
        .selector('edge')
        .css({
            'curve-style': 'bezier',
            // 'target-arrow-shape': 'triangle',
            'width': 4,
            'line-color': '#ddd',
            'target-arrow-color': '#ddd',
            'label': 'data(label)'
        })
        .selector('.highlighted')
        .css({
            'background-color': '#ff6666',
            'line-color': '#3399cc',
            'target-arrow-color': '#61bffc',
            'transition-property': 'background-color, line-color, target-arrow-color',
            'transition-duration': '0.5s'
        })
        .selector('.newStyle')
        .css({
            'background-color': '#000000',
            'line-color': '#000000',
            'target-arrow-color': '#61bffc',
            'transition-property': 'background-color, line-color, target-arrow-color',
            'transition-duration': '0.5s'
        })
        .selector(':selected')
        .css({
            'background-color': 'black',
            'line-color': 'black',
            'target-arrow-color': 'black',
            'source-arrow-color': 'black',
            'opacity': 1
        }),
    elements: {

        nodes: cytoscapeNodes,
        edges: cytoscapeEdges

        //nodes: [
        //    { data: { id: 'S1', label: '192.168.0.7' } },
        //    { data: { id: 'S2', label: '192.168.0.8' } },
        //    { data: { id: 'S3', label: '192.168.0.9' } },
        //    { data: { id: 'S4', label: '192.168.0.10' } },
        //    { data: { id: 'R11', label: '192.168.1.11' } }
        //],
        //edges: [
        //    { data: { id: 'S1R1', weight: 2, source: 'S1', target: 'R1' } },
        //    { data: { id: 'R6R7', weight: 10, source: 'R6', target: 'R7' } },
        //    { data: { id: 'R7R10', weight: 4, source: 'R7', target: 'R10' } },
        //    { data: { id: 'R10R11', weight: 1, source: 'R10', target: 'R11' } }

        //]
    },


    layout: {
        name: 'cose',
        // directed: true,
        roots: '#R11',
        padding: 30
    },
    // layout: {
    //     name: 'cose',
    //     // directed: true,
    //     roots: '#R11',
    //     padding: 30
    // },
    // layout: {
    //     name: 'preset'
    // },

});


// cy.autolock( true );
cy.userZoomingEnabled(false);
//var bfs = cy.elements().dfs('#S1', function () { }, true);
//var i = 0;
//var path = new Array("S1", "R1", "R11");
// var highlightNextEle = function(){
//     if( i <bfs.path.length ){
//         bfs.path[i].addClass('highlighted');
//
//         i++;
//         flag=setTimeout(highlightNextEle, 1000);
//     }
var flag;
var i = 0;
function highlightNextEle(path) {
    if (i < path.length) {
        cy.$('#' + path[i]).addClass('highlighted');
        if ((i + 1) != path.length) {
            cy.$('#' + path[i] + path[i + 1]).addClass('highlighted');
        }
        i++;
        flag = setTimeout(highlightNextEle, 1000, path);
    }
};



