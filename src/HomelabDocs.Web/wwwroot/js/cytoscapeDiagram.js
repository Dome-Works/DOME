import cytoscape from '../lib/cytoscape/cytoscape.esm.min.js';

let cy = null;
let dotNetRef = null;

const stylesheet = [
  {
    selector: 'node',
    style: {
      'background-color': '#1c2330',
      'border-width': 2,
      'border-color': '#8b9bb0',
      'color': '#e6edf3',
      'label': 'data(label)',
      'text-valign': 'center',
      'text-halign': 'center',
      'font-size': 11,
      'text-wrap': 'wrap',
      'text-max-width': 90,
      'width': 90,
      'height': 42,
      'overlay-padding': 4
    }
  },
  {
    selector: 'node[type = "host"]',
    style: {
      'shape': 'round-rectangle',
      'background-color': '#243044',
      'border-color': '#3d8bfd',
      'width': 130,
      'height': 52,
      'font-size': 12,
      'font-weight': 600
    }
  },
  {
    selector: 'node[type = "container"]',
    style: {
      'shape': 'rectangle',
      'background-color': '#1a2738',
      'border-color': '#5a9fff'
    }
  },
  {
    selector: 'node[type = "network"]',
    style: {
      'shape': 'diamond',
      'background-color': '#1e2a24',
      'border-color': '#3ecf8e',
      'width': 100,
      'height': 100
    }
  },
  {
    selector: 'node[type = "volume"]',
    style: {
      'shape': 'ellipse',
      'background-color': '#2a2418',
      'border-color': '#d4a017',
      'width': 100,
      'height': 56
    }
  },
  {
    selector: 'node[status = "running"]',
    style: {
      'border-color': '#3ecf8e',
      'border-width': 3
    }
  },
  {
    selector: 'node[status = "stopped"]',
    style: {
      'border-color': '#6b7785',
      'background-color': '#151a20',
      'color': '#8b9bb0',
      'border-width': 2,
      'opacity': 0.75
    }
  },
  {
    selector: 'node[status = "unhealthy"]',
    style: {
      'border-color': '#e6a23c',
      'border-width': 3,
      'background-color': '#2a2214'
    }
  },
  {
    selector: 'node:selected',
    style: {
      'border-color': '#ffffff',
      'border-width': 3
    }
  },
  {
    selector: 'edge',
    style: {
      'width': 1.5,
      'line-color': '#3a4a60',
      'target-arrow-color': '#3a4a60',
      'target-arrow-shape': 'triangle',
      'curve-style': 'bezier',
      'arrow-scale': 0.8,
      'label': 'data(label)',
      'font-size': 9,
      'color': '#8b9bb0',
      'text-rotation': 'autorotate',
      'text-margin-y': -8
    }
  }
];

function toElements(graph) {
  const nodes = (graph?.nodes ?? []).map((node) => ({
    group: 'nodes',
    data: {
      id: node.id,
      label: node.label,
      type: node.type,
      status: node.status ?? ''
    }
  }));

  const edges = (graph?.edges ?? []).map((edge) => ({
    group: 'edges',
    data: {
      id: edge.id,
      source: edge.source,
      target: edge.target,
      label: edge.label ?? ''
    }
  }));

  return [...nodes, ...edges];
}

export function initialize(element, graph, dotNetHelper) {
  if (cy) {
    return;
  }

  if (!element) {
    throw new Error('Cytoscape container element is required.');
  }

  dotNetRef = dotNetHelper;

  cy = cytoscape({
    container: element,
    elements: toElements(graph),
    style: stylesheet,
    layout: {
      name: 'cose',
      animate: false,
      padding: 40,
      nodeRepulsion: 8000,
      idealEdgeLength: 120
    },
    minZoom: 0.2,
    maxZoom: 3,
    wheelSensitivity: 0.25
  });

  cy.on('tap', 'node', (event) => {
    const nodeId = event.target.id();
    if (dotNetRef) {
      dotNetRef.invokeMethodAsync('NotifyNodeSelectedAsync', nodeId);
    }
  });

  cy.on('tap', (event) => {
    if (event.target === cy && dotNetRef) {
      dotNetRef.invokeMethodAsync('NotifyNodeSelectedAsync', null);
    }
  });
}

export function dispose() {
  if (cy) {
    cy.destroy();
    cy = null;
  }

  dotNetRef = null;
}
