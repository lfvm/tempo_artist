//Scripts para manejar la pantalla del login 

const handleLogin = async(e) => {

    e.preventDefault();

    const loginForm = document.getElementById('login_form');

    const email = loginForm[0].value;
    const password = loginForm[1].value;

    if (email === '' || password === '') {
        redHighlights(email, password);
        
        if (alertInScreen())
            addAlert("Please fill in all the required fields.");
        else 
            shakeAlert();
        
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
    div.appendChild(document.createTextNode(text))

    form.appendChild(div)
}

function redHighlights(email, password){
    // TODO: Limpiar este codigo
    let email_input = document.getElementById("exampleInputEmail1");
    let password_input = document.getElementById("exampleInputPassword1");

    let email_label = document.querySelector("label[for=exampleInputEmail1]");
    let password_label = document.querySelector("label[for=exampleInputPassword1]");


    email_input.setAttribute("class", "form-control");
    password_input.setAttribute("class", "form-control");
    email_label.classList.remove("class", "set-red-label");
    password_label.classList.remove("class", "set-red-label");

    if (email === ''){
        email_input.classList.add("class", "is-invalid")
        email_label.classList.add("class", "set-red-label")
    }
    if (password === ''){
        password_input.classList.add("class", "is-invalid");
        password_label.classList.add("class", "set-red-label")
    }
}

function alertInScreen(){
    let form = document.getElementById('login_form');
    return form.lastElementChild.className == "login_btn";
}

function shakeAlert(){
    let alert = document.getElementsByClassName("alert alert-danger alert-dismissible fade show");

    alert[0].classList.add("apply-shake")
    
    // Remover la animacion para que aparezca cada que se de click
    alert[0].addEventListener("animationend", (e) => {
        alert[0].classList.remove("apply-shake");
    }); 
}

function main(){
    type_effect()
}

