const express = require('express');
const { check } = require('express-validator');
const { getAllPunctuations, createPunctuation } = require('../../controllers/puntuations');
const { validateRequestFields } = require('../../middlewares/validar_campos');
let router = express.Router();


//Archivo para manejar operaciones CRUD con los usuarios



//Ruta para obtener todas las puntuaciones
router.get('/', getAllPunctuations);

router.post('/nueva',[

    check('id_usuario', "el id del usuario es obligatorio").not().isEmpty(),
    check('id_nivel',   "el id del nivel es obligatorio").not().isEmpty(),
    check('total_puntos',"los puntos son obligatorios").not().isEmpty(),
    check('fecha', "la fecha es obligatoria").not().isEmpty(),


    validateRequestFields
], createPunctuation);





module.exports = router;
