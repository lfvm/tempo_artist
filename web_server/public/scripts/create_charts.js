const loadData = async () => {

    //Hacer peticion a la api y mandar el id del usuario
    const id = localStorage.getItem('id');
    const response = await fetch(`/api/puntuaciones/usuario/${id}`)
        .then(res => res.json())
        .then(res => {
            
            //Verificar que la respuesta sea correcta, y si es asi llenar la tabla con los datos
            if (res['status'] == "success") {

                //Create first graph
                plotGraph(res['scores']);
                
                levels = [];
                index = 0;
                res['scores'].forEach(score => {
                    if ( ! levels.includes(score.name)) {
                        levels.push(score.name);

                        // Get the div 
                        div = document.getElementById("scrollH");
                        
                        // Create element to insert
                        canva = document.createElement("canvas");
                        canva.setAttribute("id", `pieChartHits${index}`);

                        //Insert the element and create the chart
                        div.appendChild(canva);
                        pieChartGlobalHits(res['scores'], score.name, `pieChartHits${index}`);

                        index++;
                    }
                })
            }
        });
}


const plotGraph = (scores) => {

    //obtener los total points del usuario

    data = [];
    labels = [];

    scores.forEach(score => {
        
        if (labels.includes(score.name)) {
            // Comparamos con el valor que se encuentra en la label actual
            if ( score.data > data[labels.indexOf(score.name)] ) {
                data[labels.indexOf(score.name)] = score.data
            }
        } else {
            data.push([score.total_points]);
            labels.push(score.name);
        }  

    });


    const ctx = document.getElementById('myChart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: "Score",
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            title : {
                display: true,
                text: "High Score Per Level",
            }
        }
    });
}

const pieChartGlobalHits = (scores, level, id) => {
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
    const ctx = document.getElementById(id).getContext('2d');

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