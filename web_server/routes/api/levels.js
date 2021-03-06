const express = require('express');
const { check } = require('express-validator');
const { getAllLevels, createLevel, updateLevelById } = require("../../controllers/levels")
const { validateRequestFields } = require('../../middlewares/validar_campos');

let router = express.Router();

//Archivo para manejar las rutas de los niveles


router.get("/", getAllLevels);


router.post('/nuevo',[

    check('name' , 'el nombre del nivel es obligatorio').not().isEmpty(),
    check('difficulty' ,'la dificultad del nivel es obligatoria').not().isEmpty(),
    check('total_notes' ,'las notas totales del son obligatorias').not().isEmpty(),
    check('author' ,'las notas totales del son obligatorias').not().isEmpty(),
    check('level_type' ,'las notas totales del son obligatorias').not().isEmpty(),


    validateRequestFields
],createLevel);


router.put('/:id',[
    
    check('id', 'El id es obligatorio').not().isEmpty(),
    check('name' , 'el nombre del nivel es obligatorio').not().isEmpty(),
    check('difficulty' ,'la dificultad del nivel es obligatoria').not().isEmpty(),
    check('total_notes' ,'las notas totales del son obligatorias').not().isEmpty(),
    check('author' ,'las notas totales del son obligatorias').not().isEmpty(),
    check('level_type' ,'las notas totales del son obligatorias').not().isEmpty(),

    validateRequestFields
],updateLevelById);


module.exports = router;