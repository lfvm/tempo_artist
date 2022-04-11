const express = require('express');
const { getAllPunctuations } = require('../../controllers/puntuations');
let router = express.Router();


//Archivo para manejar operaciones CRUD con los usuarios



//Ruta para obtener todas las puntuaciones
router.get('/', getAllPunctuations);




module.exports = router;
