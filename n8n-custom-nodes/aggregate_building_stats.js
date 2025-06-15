const descriptors = $input.all();

// Helper values
let total = descriptors.length;
let totalHeight = 0;
let heights = [];
let minHeight = Infinity;
let maxHeight = -Infinity;

const levelCounts = {};
const facadeCounts = {};

descriptors.forEach( d => {
    const desc = d.json;

    // Building height stats
    const height = desc.buildingHeight ?? 0;
    heights.push( height );
    totalHeight += height;
    minHeight = Math.min( minHeight, height );
    maxHeight = Math.max( maxHeight, height );

    // Building levels distribution
    const levels = desc.buildingLevels ?? 'unknown';
    levelCounts[levels] = ( levelCounts[levels] || 0 ) + 1;

    // Facade material distribution
    const material = desc.buildingFacadeMaterial ?? 'unknown';
    facadeCounts[material] = ( facadeCounts[material] || 0 ) + 1;
} );

// Final result
const averageHeight = total > 0 ? totalHeight / total : 0;

return [
    {
        json: {
            totalBuildings: total,
            averageBuildingHeight: averageHeight,
            minBuildingHeight: minHeight,
            maxBuildingHeight: maxHeight,
            buildingLevelsDistribution: levelCounts,
            facadeMaterialDistribution: facadeCounts
        }
    }
];
