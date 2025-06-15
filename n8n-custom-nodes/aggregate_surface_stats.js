const surfaces = $input.all();

let totalPaths = surfaces.length;
let totalWidth = 0;
let minWidth = Infinity;
let maxWidth = -Infinity;

let totalLanesForward = 0;
let totalLanesBackward = 0;
let markedCount = 0;

const pathTypeDist = {};
const pathMaterialDist = {};
const intersectionMaterialDist = {};

surfaces.forEach( s => {
    const data = s.json;

    // Width stats
    const width = data.width ?? 0;
    totalWidth += width;
    minWidth = Math.min( minWidth, width );
    maxWidth = Math.max( maxWidth, width );

    // Lane counts
    totalLanesForward += data.lanesForward ?? 0;
    totalLanesBackward += data.lanesBackward ?? 0;

    // Marked count
    if ( data.isRoadwayMarked ) markedCount++;

    // Path type distribution
    const pathType = data.pathType ?? 'unknown';
    pathTypeDist[pathType] = ( pathTypeDist[pathType] || 0 ) + 1;

    // Path material distribution
    const pathMat = data.pathMaterial ?? 'unknown';
    pathMaterialDist[pathMat] = ( pathMaterialDist[pathMat] || 0 ) + 1;

    // Intersection material distribution
    const intMat = data.intersectionMaterial ?? 'unknown';
    intersectionMaterialDist[intMat] = ( intersectionMaterialDist[intMat] || 0 ) + 1;
} );

const avgWidth = totalPaths > 0 ? totalWidth / totalPaths : 0;
const markedRatio = totalPaths > 0 ? markedCount / totalPaths : 0;

return [{
    json: {
        totalPaths,
        averageWidth: avgWidth,
        minWidth,
        maxWidth,
        totalLanesForward,
        totalLanesBackward,
        isRoadwayMarkedRatio: markedRatio,
        pathTypeDistribution: pathTypeDist,
        pathMaterialDistribution: pathMaterialDist,
        intersectionMaterialDistribution: intersectionMaterialDist
    }
}];
