const loadData = async () => {

    //Hacer peticion a la api y mandar el id del usuario
    const id = localStorage.getItem('id');
    const response = await fetch(`/api/puntuaciones/usuario/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
            //Verificar que la respuesta sea correcta, y si es asi llenar la tabla con los datos
            if (res['status'] == "success") {

                plotGraph(res['scores']);
                pieChartGlobalHits(res['scores'], "getting starte");
            }

        });


}


const plotGraph = (scores) => {

    //obtener los total points del usuario

    data = [];
    labels = [];

    scores.forEach(score => {
        data.push([score.total_points]);
        labels.push(score.name);
    });


    const ctx = document.getElementById('myChart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Total Scores per level',
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

const pieChartGlobalHits = (scores, level) => {
    // TODO: Hace que funcione con todos los cualquier nivel
    const level_name = `Level: ${level}`
    
    // Almacenar los tipos de hits
    perfect = [];
    good = [];
    missed = []; 
    
    scores.forEach(score => {
        if (score.name == level) {
            perfect.push(score.perfect_hits);
            good.push([score.good_hits]);
            missed.push([score.total_notes - (score.good_hits - score.perfect_hits)]);   
        }
    });

    // Obtener el promedio
    perfect_m = perfect.reduce((a, b) => parseInt(a) + parseInt(b)) / perfect.length;
    good_m = good.reduce((a, b) => parseInt(a) + parseInt(b)) / good.length;
    missed_m = missed.reduce((a, b) => parseInt(a) + parseInt(b)) / missed.length;

    
    // Generar el grafico
    const ctx = document.getElementById('pieChartHits').getContext('2d');

    const pieChart1 = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ["Perfect", "Good", "Missed"],
            datasets: [{
                label: "Average hits by level",
                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f"],
                data: [perfect_m, good_m, missed_m]
            }]
        },
        options: {
            title: {
                display: true,
                text: level_name,
            }
        }
    })
}