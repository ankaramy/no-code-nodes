const items = $input.all();

const sentences = items.map( item => {
    const data = item.json;
    let buildingType = data.buildingType;
    let buildingName = data.buildingName;

    const hasType = buildingType && buildingType !== 'unknown';
    const hasName = buildingName && buildingName !== 'unnamed';

    let sentence = "";

    if ( hasType && hasName ) {
        sentence = `A ${ buildingType } building named ${ buildingName }.`;
    } else if ( hasType && !hasName ) {
        sentence = `A ${ buildingType } building with no specific name.`;
    } else if ( !hasType && hasName ) {
        sentence = `A building named ${ buildingName }.`; // Updated logic
    } else {
        sentence = "A building with no specific name.";
    }

    return { json: { sentence } };
} );

return sentences;
