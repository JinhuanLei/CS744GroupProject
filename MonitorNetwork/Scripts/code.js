var flag;
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

    // elements: {
    //     nodes: [
    //         { data: { id: 'a' ,label:'192.168.1.1'} },
    //         { data: { id: 'b' } },
    //         { data: { id: 'c' } },
    //         { data: { id: 'd' } },
    //         { data: { id: 'e' } }
    //     ],
    //
    //     edges: [
    //         { data: { id: 'c1', weight: 1, source: 'a' , target: 'e',label:'5'} },
    //         { data: { id: 'c2', weight: 3, source: 'a', target: 'b' } },
    //         { data: { id: 'c3', weight: 4, source: 'a', target: 'c' } },
    //         { data: { id: 'c4', weight: 5, source: 'b', target: 'd' } },
    //         { data: { id: 'c5', weight: 6, source: 'c', target: 'd' } },
    //         { data: { id: 'c6', weight: 2, source: 'b', target: 'e' } },
    //         { data: { id: 'c7', weight: 7, source: 'd', target: 'e' } }
    //     ]
    // },
    elements: {
        nodes: [
            { data: { id: 'S1', label: '192.168.0.7' }, position: { x: 100, y: 50 } },
            { data: { id: 'S2', label: '192.168.0.8' }, position: { x: 500, y: 50 } },
            { data: { id: 'S3', label: '192.168.0.9' }, position: { x: 500, y: 50 } },
            { data: { id: 'S4', label: '192.168.0.10' } },
            { data: { id: 'S5', label: '192.168.0.21' } },
            { data: { id: 'S6', label: '192.168.0.11' } },
            { data: { id: 'S7', label: '192.168.0.12' } },
            { data: { id: 'S8', label: '192.168.0.13' } },
            { data: { id: 'S9', label: '192.168.0.14' } },
            { data: { id: 'S10', label: '192.168.0.22' } },
            { data: { id: 'S11', label: '192.168.0.15' } },
            { data: { id: 'S12', label: '192.168.0.16' } },
            { data: { id: 'S13', label: '192.168.0.17' } },
            { data: { id: 'S14', label: '192.168.0.23' } },
            { data: { id: 'S15', label: '192.168.0.18' } },
            { data: { id: 'S16', label: '192.168.0.19' } },
            { data: { id: 'S17', label: '192.168.0.20' } },
            { data: { id: 'S18', label: '192.168.0.24' } },
            { data: { id: 'S19', label: '192.168.0.21' } },
            { data: { id: 'S20', label: '192.168.0.25' } },
            { data: { id: 'S21', label: '192.168.0.26' } },
            { data: { id: 'S22', label: '192.168.0.27' } },
            { data: { id: 'S23', label: '192.168.0.28' } },
            { data: { id: 'S24', label: '192.168.0.29' } },
            { data: { id: 'S25', label: '192.168.0.30' } },
            { data: { id: 'S26', label: '192.168.0.31' } },
            { data: { id: 'S27', label: '192.168.0.32' } },
            { data: { id: 'S28', label: '192.168.0.33' } },
            { data: { id: 'S29', label: '192.168.0.34' } },
            { data: { id: 'S30', label: '192.168.0.35' } },
            { data: { id: 'R1', label: '192.168.1.1' } },
            { data: { id: 'R2', label: '192.168.1.2' } },
            { data: { id: 'R3', label: '192.168.1.3' } },
            { data: { id: 'R4', label: '192.168.1.4' } },
            { data: { id: 'R5', label: '192.168.1.5' } },
            { data: { id: 'R6', label: '192.168.1.6' } },
            { data: { id: 'R7', label: '192.168.1.7' } },
            { data: { id: 'R8', label: '192.168.1.8' } },
            { data: { id: 'R9', label: '192.168.1.9' } },
            { data: { id: 'R10', label: '192.168.1.10' } },
            { data: { id: 'R11', label: '192.168.1.11' } }


        ],

        edges: [
            { data: { id: 'S1R1', weight: 2, source: 'S1', target: 'R1' } },
            { data: { id: 'S2R1', weight: 3, source: 'S2', target: 'R1' } },
            { data: { id: 'S2R2', weight: 1, source: 'S2', target: 'R2' } },
            { data: { id: 'S3R2', weight: 4, source: 'S3', target: 'R2' } },
            { data: { id: 'S4R2', weight: 3, source: 'S4', target: 'R2' } },
            { data: { id: 'S5R2', weight: 5, source: 'S5', target: 'R2' } },
            { data: { id: 'S6R3', weight: 7, source: 'S6', target: 'R3' } },
            { data: { id: 'S7R3', weight: 5, source: 'S7', target: 'R3' } },
            { data: { id: 'S8R4', weight: 6, source: 'S8', target: 'R4' } },
            { data: { id: 'S9R4', weight: 9, source: 'S9', target: 'R4' } },
            { data: { id: 'S10R4', weight: 8, source: 'S10', target: 'R4' } },
            { data: { id: 'S11R4', weight: 10, source: 'S11', target: 'R4' } },
            { data: { id: 'S12R4', weight: 6, source: 'S12', target: 'R4' } },
            { data: { id: 'S13R5', weight: 7, source: 'S13', target: 'R5' } },
            { data: { id: 'S14R5', weight: 8, source: 'S14', target: 'R5' } },
            { data: { id: 'S15R5', weight: 3, source: 'S15', target: 'R5' } },
            { data: { id: 'S16R6', weight: 2, source: 'S16', target: 'R6' } },
            { data: { id: 'S17R7', weight: 1, source: 'S17', target: 'R7' } },
            { data: { id: 'S18R7', weight: 4, source: 'S18', target: 'R7' } },
            { data: { id: 'S18R8', weight: 3, source: 'S18', target: 'R8' } },
            { data: { id: 'S19R8', weight: 2, source: 'S19', target: 'R8' } },
            { data: { id: 'S20R8', weight: 7, source: 'S20', target: 'R8' } },
            { data: { id: 'S21R8', weight: 6, source: 'S21', target: 'R8' } },
            { data: { id: 'S22R8', weight: 5, source: 'S22', target: 'R8' } },
            { data: { id: 'S23R8', weight: 3, source: 'S23', target: 'R8' } },
            { data: { id: 'S24R8', weight: 8, source: 'S24', target: 'R8' } },
            { data: { id: 'S25R8', weight: 5, source: 'S25', target: 'R8' } },
            { data: { id: 'S26R10', weight: 4, source: 'S26', target: 'R10' } },
            { data: { id: 'S27R10', weight: 6, source: 'S27', target: 'R10' } },
            { data: { id: 'S28R10', weight: 10, source: 'S28', target: 'R10' } },
            { data: { id: 'S29R10', weight: 9, source: 'S29', target: 'R10' } },
            { data: { id: 'S30R10', weight: 3, source: 'S30', target: 'R10' } },
            { data: { id: 'R1R11', weight: 2, source: 'R1', target: 'R11' } },
            { data: { id: 'R2R3', weight: 8, source: 'R2', target: 'R3' } },
            { data: { id: 'R3R5', weight: 6, source: 'R3', target: 'R5' } },
            { data: { id: 'R4R5', weight: 3, source: 'R4', target: 'R5' } },
            { data: { id: 'R5R11', weight: 2, source: 'R5', target: 'R11' } },
            { data: { id: 'R6R9', weight: 1, source: 'R6', target: 'R9' } },
            { data: { id: 'R6R10', weight: 5, source: 'R6', target: 'R10' } },
            { data: { id: 'R6R7', weight: 10, source: 'R6', target: 'R7' } },
            { data: { id: 'R7R10', weight: 4, source: 'R7', target: 'R10' } },
            { data: { id: 'R7R8', weight: 1, source: 'R7', target: 'R8' } },
            { data: { id: 'R8R10', weight: 7, source: 'R8', target: 'R10' } },
            { data: { id: 'R9R11', weight: 3, source: 'R9', target: 'R11' } },
            { data: { id: 'R10R11', weight: 1, source: 'R10', target: 'R11' } }

        ]
    },


    layout: {
        name: 'breadthfirst',
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
var bfs = cy.elements().dfs('#S1', function () { }, true);
var i = 0;
var path = new Array("S1", "R1", "R11");
// var highlightNextEle = function(){
//     if( i <bfs.path.length ){
//         bfs.path[i].addClass('highlighted');
//
//         i++;
//         flag=setTimeout(highlightNextEle, 1000);
//     }
var highlightNextEle = function () {
    if (i < path.length) {
        cy.$('#' + path[i]).addClass('highlighted');

        i++;
        flag = setTimeout(highlightNextEle, 1000);
    }


};



