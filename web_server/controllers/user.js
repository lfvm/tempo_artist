//Archivo para manejar las fucniones de usuarios en la api
// Como crear, actualizar, eliminar y obtener usuarios
//Llamar estas funciones en las rutas corresqondientes
const connection = require('../db/db_config');

const getAllUsers = (req,res) => {
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
}

const createUser =  async( req, res ) => {

    //Guardar el usuaior en la base de datos

    let date;
    let cliente = 'cliente'
    let today = new Date().toLocaleDateString()

    let user  = req.body;


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


    connection.query(query, function(err, rows, fields) {

        if (err) {

            console.log( err);
            return res.json({
                msg: "error",
                error
            });
        }
            
        return res.json({
            msg: "ok",
            user
        });
    });
        

    return res.json({
        msg: "error",
        error
    });
        
    
   

}

const logUserIn = async(req,res) => {


    const {correo ,password} = req.body;

    const query = `SELECT * FROM usuarios WHERE 
    (correo='${correo}' AND contraseña='${password}');
    `


    connection.query(query, function(err, rows, fields) {

        if (err) {
            console.log( err);
            return res.json({
                msg: "error",
                error
            });
        }   
        
        //Verfifcar que exista un usuario con ese mail y contraseña
        if(rows.length < 1 ){

            return res.json({
                status: "auth error",
                msg: "Incorrect email or password"
            });

        }

        //Mandar mensaje de exito
        const user = rows[0]
        return res.json({

            status: "succes",
            user
        });
    
 
    });
        




}


module.exports = {

    createUser,
    logUserIn,
    getAllUsers

}