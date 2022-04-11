const express = require('express');
const { createUser, getAllUsers, logUserIn, getUserById } = require('../../controllers/user');
let router = express.Router();
const { check } = require('express-validator');
const { validateRequestFields } = require('../../middlewares/validar_campos');

//Archivo para manejar operaciones CRUD con las puntuaciones


//Ruta para obtener todos los usuarios
router.get('/', getAllUsers);

router.get('/:id',[
    check('id', 'El id es obligatorio').not().isEmpty(),
], getUserById);



//Ruta para crear un usuario
router.post('/nuevo', [

    check('correo', 'El correo es obligatorio').isEmail(),
    check('password', 'La contraseña es obligatoria').not().isEmpty(),
    check('nombre', 'el nombre es obligatorio').not().isEmpty(),
    check('gender', 'El genero es obligatiorio').not().isEmpty(),
    check('apellidos', 'El apellido es obligatorio').not().isEmpty(),
    check('edad', 'La edad es obligatoria').not().isEmpty(),
    check('instrumento', 'el instrumento es obligatorio').not().isEmpty(),
    validateRequestFields

],createUser);

router.post('/login', [
    check('correo', 'El correo es obligatorio').isEmail(),
    check('password', 'La contraseña es obligatoria').not().isEmpty(),
    validateRequestFields
],logUserIn);





module.exports = router;