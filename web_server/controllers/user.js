//Archivo para manejar las fucniones de usuarios en la api
// Como crear, actualizar, eliminar y obtener usuarios
//Llamar estas funciones en las rutas corresqondientes
const connection = require('../db/db_config');

const createUser =  async( user ) => {

    //Guardar el usuaior en la base de datos
    connection.connect();

    let date;
    let cliente = 'cliente'
    let today = new Date().toLocaleDateString()


    const query = `INSERT INTO usuarios (
        nombre,
        apellidos,
        correo,
        contraseña,
        genero,
        fecha_creacion,
        rol,
        edad,
        toca_instrumento
    )VALUES('${user.nombre}','${user.apellidos}','${user.correo}','${user.password}','${user.gender}','${date}','${cliente}','${user.edad}',${true});`


    await connection.query(query, function(err, rows, fields) {

        if (err) {

            console.log( err);
            connection.destroy();
            return false
        }
        connection.destroy();
        return rows;
        
    });

}



module.exports = {

    createUser

}