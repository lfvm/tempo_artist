const express = require('express');
const { check } = require('express-validator');
const { getAllPunctuations, createPunctuation, getUserScores } = require('../../controllers/scores');
const { validateRequestFields } = require('../../middlewares/validar_campos');
let router = express.Router();


//Archivo para manejar operaciones CRUD con los usuarios



//Ruta para obtener todas las puntuaciones
router.get('/', getAllPunctuations);

router.post('/nueva',[

    check('user_id', "el id del usuario es obligatorio").not().isEmpty(),
    check('level_id',   "el id del nivel es obligatorio").not().isEmpty(),
    check('total_points',"los puntos son obligatorios").not().isEmpty(),
    check('perfect_hits',"las notas perfcectas son obligatorias").not().isEmpty(),
    check('good_hits',"las notas correctas son obligatorias").not().isEmpty(),
    check('accuracy',"La precision es obligatoria").not().isEmpty(),
    check('max_combo',"El combo es obligatorios").not().isEmpty(),



    validateRequestFields
], createPunctuation);


router.get('/usuario/:id',[
    check("id", "El id del usuario es obligatorio").not().isEmpty(),
], getUserScores);




module.exports = router;
