CREATE DATABASE geodb;

use geodb;

CREATE TABLE geodb.geo_localizacion (
	id INT NOT NULL AUTO_INCREMENT,
    calle VARCHAR(64) NOT NULL,
    numero VARCHAR(64),
    ciudad VARCHAR(64),
    codigo_postal VARCHAR(64),
    provincia VARCHAR(64),
    pais VARCHAR(64),
    latitud FLOAT(17,15),
    longitud FLOAT(18,15),
    procesando BIT NOT NULL DEFAULT 0,
    osm_id int,
    PRIMARY KEY (id)
);