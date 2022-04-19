const connection = require("../db/db_config")


const getAllLevels = (req, res) => {

    connection.query("SELECT * FROM niveles", (err, rows, fields) => {
        if (!err) {
            
            res.json({
                status: "success",
                niveles: rows
            });

        } else {

            res.json({
                status: "fail",
                msg: err
            });
        }
    });
}



const createLevel = (req, res) => {

    const level = req.body;

    const query = `
    INSERT INTO niveles (
    duración, 
    nombre
    )VALUES(
        '${level.duración}',
        '${level.nombre}'
    );
    `


    connection.query(query, (err, rows, fields) => {

        if (!err) {
            
            res.json({
                status: "success",
                nivel: level,
                id: rows.insertId
            });

        } else {

            res.json({
                status: "fail",
                msg: err
            });
        }

    });
    
}



module.exports ={

    getAllLevels,
    createLevel

}