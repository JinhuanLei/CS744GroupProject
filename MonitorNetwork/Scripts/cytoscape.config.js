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
        .selector("node[id^='s']")
        .css({
            'label': 'data(label)',
            'shape': 'triangle'
        })
        .selector("node[id^='r']")
        .css({
            'label': 'data(label)',
            'background-color': '#000000'
        })
        .selector("node[id^='p']")
        .css({
            'label': 'data(label)',
            'background-color': 'data(label)'
        })
        .selector("node[id='r11']")
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
   // nodeDimensionsIncludeLabels: true,
   edgeDimensionsIncludeLabels: true,
    roots: '#r11'
});
layout.run();
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
cy.nodes().qtip({

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



