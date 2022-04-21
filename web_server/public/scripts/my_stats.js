
const getScores = async() => {

    //Hacer peticion a la api y mandar el id del usuario
    const id = localStorage.getItem('id');
    const response = await fetch(`/api/puntuaciones/usuario/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
            //Verificar que la respuesta sea correcta, y si es asi llenar la tabla con los datos
            if(res['status'] == "success"){
                
                fillData(res['scores']);
            }

        });


}

const fillData = (scores) => {
    //Funcion que llena la tabla con la informacion de los scores

    //Obtenemos el elemento donde se va a llenar la tabla
    const table = document.getElementById('scores');

    //Crear una nueva fila con los datos del score y agregar a la tabla
    scores.forEach(score => {
        const row = table.insertRow();
        row.insertCell().innerHTML = score.name;
        row.insertCell().innerHTML = score.total_points;
        row.insertCell().innerHTML = score.perfect_hits;
        row.insertCell().innerHTML = score.good_hits;
        row.insertCell().innerHTML = score.total_notes;
        row.insertCell().innerHTML = score.total_notes - (score.good_hits - score.perfect_hits);
        row.insertCell().innerHTML = score.accuracy + '%';
        row.insertCell().innerHTML = score.created_at.slice(0,10);

    });

}


const main = async(e) => {
    await getScores();
}

//Respuesta para obtener los scores
// {
//     "status": "success",
//     "scores": [
//         {
//             "total_points": 1230,
//             "perfect_hits": 1230,
//             "good_hits": 122,
//             "total_notes": 324, 
//             "created_at": "2022-04-21T04:18:51.000Z",
//             "accuracy": 60,
//             "max_combo": 3321,
//             "name": "getting started"
//         }
//     ]
// }