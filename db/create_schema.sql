-- Script para la creacion del esquema de la base de datos para Tempo Artist



-- Crea tabla usuarios
CREATE TABLE `usuarios`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `nombre` VARCHAR(255) NOT NULL,
    `apellidos` VARCHAR(255) NOT NULL,
    `correo` VARCHAR(255) NOT NULL,
    `contraseña` VARCHAR(255) NOT NULL,
    `genero` CHAR(255) NOT NULL,
    `fecha_creacion` DATETIME NOT NULL,
    `rol` VARCHAR(255) NOT NULL,
    `edad` INT NOT NULL,
    `toca_instrumento` BOOLEAN
);

-- Agrega llave primaria a la tabla usuarios
ALTER TABLE
    `usuarios` ADD PRIMARY KEY `usuarios_id_primary`(`id`);


-- Crea tabla de puntuaciones
CREATE TABLE `puntuaciones`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `id_usuario` INT NOT NULL,
    `id_nivel` INT NOT NULL,
    `total_puntos` INT NOT NULL,
    `fecha` DATETIME NOT NULL
);

-- Agregar llave primaria
ALTER TABLE
    `puntuaciones` ADD PRIMARY KEY `puntuaciones_id_primary`(`id`);


-- Crea tabla de niveles
CREATE TABLE `niveles`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `duración` INT NOT NULL,
    `nombre` VARCHAR(255) NOT NULL
);



-- agregar llaves foraneas y primariasz
ALTER TABLE
    `niveles` ADD PRIMARY KEY `niveles_id_primary`(`id`);
ALTER TABLE
    `puntuaciones` ADD CONSTRAINT `puntuaciones_id_usuario_foreign` FOREIGN KEY(`id_usuario`) REFERENCES `usuarios`(`id`);
ALTER TABLE
    `puntuaciones` ADD CONSTRAINT `puntuaciones_id_nivel_foreign` FOREIGN KEY(`id_nivel`) REFERENCES `niveles`(`id`);