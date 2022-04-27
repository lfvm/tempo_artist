const connection = require("../db/db_config")


const getAllLevels = (req, res) => {

    connection.query("SELECT * FROM Levels", (err, rows, fields) => {
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
    INSERT INTO Levels (
    lenght, 
    name,
    difficulty,
    total_notes,
    author,
    level_type
    )VALUES(
        '${level.lenght}',
        '${level.name}',
        '${level.difficulty}',
        '${level.total_notes}',
        '${level.author}',
        '${level.level_type}'
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