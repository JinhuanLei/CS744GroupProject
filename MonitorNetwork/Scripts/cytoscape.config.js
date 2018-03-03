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
        .selector("node[id>='s1']")
        .css({
            'label': 'data(label)',
            'shape': 'triangle',
        })
        .selector("node[id<'s1']")
        .css({
            'label': 'data(label)',
            'background-color': '#000000',
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
    },


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
    name: 'cose',
    roots: '#r11'
});
layout.run();
cy.on('mouseover', 'node', function (event) {
    cy.elements("node[id>='s1']").qtip({
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


    cy.elements("node[id<'s1']").qtip({
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
    var edgeid = evt.target.id();
    var start = "";
    var dest = "";
    var arr = edgeid.split("r");
    if (edgeid[0] == "r") {
        start = arr[1];
        dest = arr[2];
        for (var x = 0; x < connections.length; x++) {
            if (connections[x].relayID == start && connections[x].destRelayID == dest) {
                connections[x].active = (connections[x].active == true ? false : true);
            }
        }
    } else {
        start = arr[0].substring(1);
        dest = arr[1];
        for (var x = 0; x < connections.length; x++) {
            if (connections[x].storeID == start && connections[x].destRelayID == dest) {
                connections[x].active = (connections[x].active == true ? false : true);
            }
        }
    }
    console.log($(connections)); 
})

// cy.autolock( true );
cy.userZoomingEnabled(false);

function gotoNextNode(path) {
    highlightNextEle(path);
}

var flag;
var i = 0;
var state1 = [];
//function highlightNextEle(path) {
//    if (i < path.length) {
//        cy.$('#' + path[i]).addClass('highlighted');
//        if ((i + 1) != path.length) {
//            cy.$('#' + path[i] + path[i + 1]).addClass('highlighted');
//        }
//        i++;
//        flag = setTimeout(highlightNextEle, 1000, path);
//    }
//};
function highlightNextEle(path1) {
    console.log("origin:"+path1);
    var path = parseArr(path1);
    console.log("after:"+path1);
    if (i < path.length) {
        console.log(state1.length);
        if (state1.length != 0) {
            for (var x = 0; x < state1.length; x++) {
                cy.$('#' + state1[x]).removeClass('highlighted');
            }
            state1 = [];
        }

        cy.$('#' + path[i]).addClass('highlighted');
        var ite = path[i];
        state1.push(ite);

        i++;
        flag = setTimeout(highlightNextEle, 1000,path1);
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






