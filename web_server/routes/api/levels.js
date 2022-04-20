const express = require('express');
const { check } = require('express-validator');
const { getAllLevels, createLevel } = require("../../controllers/levels")
const { validateRequestFields } = require('../../middlewares/validar_campos');

let router = express.Router();

//Archivo para manejar las rutas de los niveles


router.get("/", getAllLevels);


router.post('/nuevo',[

    check('name' , 'el nombre del nivel es obligatorio').not().isEmpty(),
    check('lenght' ,'la duracion del nivel es obligatoria').not().isEmpty(),
    check('difficulty' ,'la dificultad del nivel es obligatoria').not().isEmpty(),
    check('total_notes' ,'las notas totales del son obligatorias').not().isEmpty(),

    validateRequestFields
],createLevel);


module.exports = router;