const inputItems = $input.all();
const descriptors = [];

inputItems.forEach( item => {
    const geometries = item.json.geometry;

    if ( Array.isArray( geometries ) ) {
        geometries.forEach( geom => {
            if ( geom.geometryType === 'meshes' && geom.type === 'buildings' && Array.isArray( geom.meshes ) ) {
                geom.meshes.forEach( mesh => {
                    if ( mesh.descriptor ) {
                        descriptors.push( { json: mesh.descriptor } );
                    }
                } );
            }
        } );
    }
} );

return descriptors;
