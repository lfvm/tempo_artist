const express = require('express');
const { createUser, getAllUsers, logUserIn, updateUser } = require('../../controllers/user');
let router = express.Router();
const { check } = require('express-validator');
const { validateRequestFields } = require('../../middlewares/validar_campos');

//Archivo para manejar operaciones CRUD con las puntuaciones


//Ruta para obtener todos los usuarios
router.get('/', getAllUsers);




//Ruta para crear un usuario
router.post('/nuevo', [

    check('mail', 'El correo es obligatorio').isEmail(),
    check('password', 'La contraseña es obligatoria').not().isEmpty(),
    check('name', 'el nombre es obligatorio').not().isEmpty(),
    check('gender', 'El genero es obligatiorio').not().isEmpty(),
    check('last_name', 'El apellido es obligatorio').not().isEmpty(),
    check('age', 'La edad es obligatoria').not().isEmpty(),
    check('plays_instrument', 'el instrumento es obligatorio').not().isEmpty(),
    validateRequestFields

],createUser);



router.post('/login', [
    check('mail', 'El correo es obligatorio').isEmail(),
    check('password', 'La contraseña es obligatoria').not().isEmpty(),
    validateRequestFields
],logUserIn);




router.put('/:id', [
    check('id', 'El id es obligatorio').not().isEmpty(),
    check('mail', 'El correo es obligatorio').isEmail(),
    check('name', 'el nombre es obligatorio').not().isEmpty(),
    check('last_name', 'El apellido es obligatorio').not().isEmpty(),
    validateRequestFields
],updateUser);




module.exports = router;