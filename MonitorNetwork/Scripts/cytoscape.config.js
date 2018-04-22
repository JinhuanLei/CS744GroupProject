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
        .selector("node[name^='s']")
        .css({
            'label': 'data(label)',
            //'shape': 'triangle'
            //'shape': 'triangle'
        })
        .selector("node[name^='r']")
        .css({
            'label': 'data(label)',
            'background-color': '#3D3B3B',
            'shape': 'rectangle'
        })
        .selector("node[name^='p']")
        .css({
            'label': 'data(label)',
            'background-color': 'data(label)'
           
        })
        .selector("node[name^='c']")
        .css({
            'label': 'data(label)',
            'shape': 'roundrectangle',
            'background-color': '#FFF000',
            'width': 100,
            'height':100
        })
        .selector("node[name^='g']")
        .css({
            'label': 'data(label)',
            'shape': 'rectangle',
            'border-color': 'white',
            'border-width': 5,
            'border-opacity': 0.5,
        
            'width': 50,
            'height': 50
        })
        .selector('edge')
        .css({
            'curve-style': 'bezier',
            // 'target-arrow-shape': 'triangle',
            'width': 4,
            'line-color': '#ddd',
            'target-arrow-color': '#ddd',
            'label': 'data(weight)'
        })
        .selector('.highlighted')
        .css({
            'background-color': '#ff6666',
            'line-color': '#3399cc',
            'target-arrow-color': '#61bffc',
            'transition-property': 'background-color, line-color, target-arrow-color',
            'transition-duration': '0.5s'
        })
        .selector('.dropped')
        .css({
            'background-color': '#000000',
            'line-color': '#000000',
            'target-arrow-color': '#61bffc',
            'transition-property': 'background-color, line-color, target-arrow-color',
            'transition-duration': '0.5s'
        })
        .selector('.newStyle')
        .css({
            'background-color': '#8080ff',
            'line-color': '#8080ff',
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
    }


 //   layout: {
  //      name: 'cose',
        // directed: true,
   //     roots: '#r11',
  //      padding: 30
  //  },
    // layout: {
    //     name: 'cose',
    //     // directed: true,
    //     roots: '#r11',
    //     padding: 30
    // },
    // layout: {
    //     name: 'preset'
    // },

});

var layout = cy.elements().layout({
    name: 'cose-bilkent',
    //nodeDimensionsIncludeLabels: true,
    edgeDimensionsIncludeLabels: true,
    fit: true,
    idealEdgeLength: 100,
    //nodeRepulsion: 9000,
    //padding:500
    //edgeElasticity:0.85,
    //gravity: 1,
  
});

//cy.on('mouseover', 'node', function (event) {

//    var nodeId = event.target.id();
//    var transactionIds = [];
//    nodeQueues[nodeId].forEach(function (queueTransaction) {
//        transactionIds.push(queueTransaction.transactionId);
//    });
//    var queueStr = transactionIds.join(", ");

//    cy.elements("node[id^='s']").qtip({
//        //content: 'node IP',
//        content: nodeId + " " + queueStr,
//        show: {
//            event: event.type,
//            // ready: true,
//            solo: true
//        },
//        hide: {
//            event: 'mouseout unfocus'
//        },
//        style: {
//            classes: 'qtip-bootstrap',
//            tip: {
//                width: 16,
//                height: 8
//            }
//        }
//    }, event);


//    cy.elements("node[id^='r']").qtip({
//        //content: 'relay IP',
//        content: nodeId + " " + queueStr,
//        show: {
//            event: event.type,
//            // ready: true,
//            solo: true
//        },
//        hide: {
//            event: 'mouseout unfocus'
//        },
//        style: {
//            classes: 'qtip-bootstrap',
//            tip: {
//                width: 16,
//                height: 8
//            }
//        }
//    }, event);

//});
cy.elements("node[id!^='p']").qtip({

    content: cytoscapeToolTip,
    position: {
        my: 'top center',
        at: 'bottom center'
    },
    style: {
        classes: 'qtip-bootstrap',
        tip: {
            width: 16,
            height: 8
        }
    },
    show: {
        event: 'mouseover',
        solo: true,
        effect: function (offset) {
            $(this).slideDown(100); // "this" refers to the tooltip
        }

    },
    hide: {
        event: 'mouseout unfocus'
    }
});

cy.on('click', 'node', cytoscapeClickNode);

cy.on('click', 'edge', cytoscapeClickEdge);

// cy.autolock( true );
cy.userZoomingEnabled(false);
layout.run();



