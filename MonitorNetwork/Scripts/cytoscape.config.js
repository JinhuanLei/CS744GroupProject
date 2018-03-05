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
    name: 'cose',
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

    content: function () {
        var nodeId = this.data('id');
        var label = this.data('label');
        var transactionIds = [];
            nodeQueues[nodeId].forEach(function (queueTransaction) {
        transactionIds.push(queueTransaction.transactionId);
    });
            var queueStr = transactionIds.join(", ");
            if (nodeId[0] == "r") {
                return '192.168.' + this.data('label') + '\n'+'Queue:' + queueStr
            } else {
                var merchantName;
                stores.forEach(function (t, number, ts) {
                    console.log(t.storeID + "   " + nodeId.substring(1));
                    console.log(t.storeID == nodeId.substring(1));
                    if (t.storeID==nodeId.substring(1)) {
                      
                        merchantName = t.merchantName;
                    }
                });
                  return merchantName + ':192.168.' + label + '\n' + 'Queue:' + queueStr
            }
    
    },
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

cy.on('click', 'node', function (evt) {
    changestyle(evt, "node");
    var nodeid = evt.target.id();
    var id = (Number)(nodeid.substring(1));
    //alert(evt.target.id() + "  " + id);
    for (var x = 0; x < relays.length; x++) {
      //  alert(relays[x].relayID + "  " + id);
        if (relays[x].relayID === id && relays[x].isProcessingCenter === false) {
            relays[x].status = (relays[x].status === true ? false : true);
        }
    }
    console.log($(relays)); 
    })

console.log(stores);
cy.on('click', 'edge', function (evt) {
    //alert(evt.target.id());
    changestyle(evt, "edge");
    var edgeid = evt.target.id();
    var start = "";
    var dest = "";
    var arr = edgeid.split("r");
    var connection;
    if (edgeid[0] === "r") {
        start = arr[1];
        dest = arr[2];
        connection = connections.find(function (connection) {
            return connection.relayID === start && connection.destRelayID === dest;
        });

        
    } else {
        start = arr[0].substring(1);
        dest = arr[1];

        connection = connections.find(function (connection) {
            return connection.storeID === start && connection.destRelayID === dest;
        });
    }
    connection.active = !connection.active;

    console.log($(connections)); 
})

// cy.autolock( true );
cy.userZoomingEnabled(false);

function getStoreIpAdress(storeID) {
    console.log(storeID);
    var nodeid = storeID;
    var id = (Number)(nodeid.substring(1));
    //alert(evt.target.id() + "  " + id);
    for (var x = 0; x < stores.length; x++) {
        //  alert(relays[x].relayID + "  " + id);
        if (stores[x].storeID == id) {

            return stores[x].storeIP;
        }
    }
}
function getRelayIpAdress(relayID) {
    console.log(relayID);
    var nodeid = relayID;
    var id = (Number)(nodeid.substring(1));
    //alert(evt.target.id() + "  " + id);
    for (var x = 0; x < relays.length; x++) {
        //  alert(relays[x].relayID + "  " + id);
        if (relays[x].relayID == id) {
            return relays[x].relayIP;
        }
    }
}

function changestyle(evt, type) {
    if (evt.target.id() != "R11") {


        if (cy.$("#" + evt.target.id()).hasClass('newStyle') == false) {
            cy.$("#" + evt.target.id()).classes('newStyle');
        }
        else {
            cy.$("#" + evt.target.id()).classes(type);
        }
    }
}



