const express = require('express');
const { createUser } = require('../../controllers/user');
let router = express.Router();
const connection = require('../../db/db_config')

//Archivo para manejar operaciones CRUD con las puntuaciones


//Ruta para obtener todos los usuarios
router.get('/', async(req, res) => {

    await connection.connect();


    connection.query(`SELECT * FROM USUARIOS`, (err, rows, fields) => {
        
        if (!err) {
            res.json({
                status: 'success', 
                rows
            });

        } else {

            res.json({
                status: 'fail', 
                'message': err
            });
        }
    });

    connection.end();


});


//Ruta para crear un usuario
router.post('/nuevo', async(req, res) => {

    const user = req.body;

    const response = await createUser(user);

    if (response != false){
        console.log(response)
        return res.json({
            status: 'Usuario creado correctamente',
            response,
        });

    }


    return res.json({
        msg: 'error al crear cuenta'
    });
  

});






module.exports = router;