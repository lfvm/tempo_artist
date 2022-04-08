//Scripts para manejar la pantalla del login 



const handleLogin = async(e) => {

    e.preventDefault();

    const loginForm = document.getElementById('login_form');

    const email = loginForm[0].value;
    const password = loginForm[1].value;

    if (email === '' || password === '') {
        //TODO: Hacer que se quite el alert
        addAlert("Please fill in all the required fields.");
    } else {

        //TODO: Enviar los datos al servidor para validar, en caso de ser correctos ir a home, de lo contrario mostrar mensaje de error
        

        //Guardar los datos en el localStorage en caso de que el login sea exitoso
        localStorage.setItem('mail', email);

        //Redirigir a home
        window.location.href = '/';
    }
}


const handleCreateAccount = async(e) => {

    e.preventDefault();
    const form = document.getElementById('create_account_form');

    //Obtener los datos del formulario
    const data = {
        correo: form[0].value,
        password: form[1].value,
        nombre: form[2].value,
        apellidos: form[3].value,
        gender: form[4].value,
        instrumento: form[5].value,
        edad: form[6].value
    }
    


    fetch('http://localhost:8080/api/usuarios/nuevo', {
        method: "POST",
        body: JSON.stringify(data),
        headers: {"Content-type": "application/json; charset=UTF-8"}
    })
        .then( res => res.json())
        .then(response => {
            
            console.log(response);

            if(response['msg'] === 'ok'){
            
                localStorage.setItem('mail',form[0].value);
                window.location.href = '/';
    
            } else {

                addAlert("There was an error while creating your account, please try again.");
            }

        })
        .catch(err => console.log(err));
}

const type_effect = async(e) => {
    let i = 0;
    let placeholder_1 = "";
    let placeholder_2 = "";
    const txt_1 = "example@mail.com";
    const txt_2 = "password";
    const speed = 150;

    function effect(){
        placeholder_1 += txt_1.charAt(i);
        placeholder_2 += txt_2.charAt(i);
        document.getElementById("exampleInputEmail1").setAttribute("placeholder", placeholder_1);
        document.getElementById("exampleInputPassword1").setAttribute("placeholder", placeholder_2);
        i++;
        setTimeout(effect, speed);
    } 
    effect();
}



function addAlert(text){
    let form = document.getElementById("login_form");

    div = document.createElement("div");

    div.setAttribute("class", "alert alert-danger alert-dismissible fade show");
    div.setAttribute("role", "alert");
    //txt = document.createTextNode("Llena todos los campos.")
    div.appendChild(document.createTextNode(text))

    document.body.appendChild(div)
}

function main(){
    type_effect()
}

