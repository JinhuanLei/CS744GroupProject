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


 //   layout: {
  //      name: 'cose',
        // directed: true,
   //     roots: '#R11',
  //      padding: 30
  //  },
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

var layout = cy.elements().layout({
    name: 'cose',
    roots: '#R11'
});
layout.run();
cy.on('mouseover', 'node', function (event) {
    cy.elements("node[id>='S1']").qtip({
        content: 'node IP',
        show: {
            event: event.type,
            // ready: true,
            solo: true
        },
        hide: {
            event: 'mouseout unfocus'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                width: 16,
                height: 8
            }
        }
    }, event);


    cy.elements("node[id<'S1']").qtip({
        content: 'relay IP',
        show: {
            event: event.type,
            // ready: true,
            solo: true
        },
        hide: {
            event: 'mouseout unfocus'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                width: 16,
                height: 8
            }
        }
    }, event);

});


cy.on('click', 'node', function (evt) {
   
    var nodeid = evt.target.id();
    var id = (Number)(nodeid.substring(1));
    //alert(evt.target.id() + "  " + id);
    for (var x = 0; x < relays.length; x++) {
      //  alert(relays[x].relayID + "  " + id);
        if (relays[x].relayID == id && relays[x].isProcessingCenter==false) {
           
    relays[x].status = (relays[x].status == true ? false : true);
        }
    }
    console.log($(relays)); 
    })


cy.on('click', 'edge', function (evt) {
    alert(evt.target.id());
    for (x in connections) {
        if (x.connID == evt.target.id()) {
            x.active = (x.active == true ? false : true);
        }
    }
    console.log($(connections)); 
})

// cy.autolock( true );
cy.userZoomingEnabled(false);

function gotoNextNode(path) {
    highlightNextEle(path, 0, []);
}

var flag;

function highlightNextEle(path1, index, state) {
    console.log("origin:" + path1);
    var path = parseArr(path1);
    console.log("after:" + path1);
    if (index < path.length) {
        console.log(state.length);
        if (state.length != 0) {
            for (var x = 0; x < state.length; x++) {
                cy.$('#' + state[x]).removeClass('highlighted');
            }
            state = [];
        }

        cy.$('#' + path[index]).addClass('highlighted');
        var ite = path[index];
        state.push(ite);

        index++;
        flag = setTimeout(highlightNextEle, 1000, path1, index, state);
    }
}


function parseArr(arr) {
    if (arr.length >= 2) {
        var newarr = [];
        for (var x = 0; x < arr.length; x++) {
            newarr.push(arr[x]);
            if (x + 1 != arr.length) {
                newarr.push(arr[x] + arr[x + 1]);
            }
        }
        return newarr;
    }
    else {
        return arr;
    }
}


