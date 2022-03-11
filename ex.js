
// function arr(ints){

//     let newInt = [];

//     ints.forEach((i)=>{
//         if(typeof i === 'number'){
//             newInt.push(i);
//         }else{
//             i.forEach((ii)=>{
//                 if(typeof ii === 'number'){
//                     newInt.push(ii);
//                 }else{
//                     ii.forEach((iii)=>{
//                         if(typeof iii === 'number'){
//                             newInt.push(iii);
//                         }else{
                            
//                         }
//                     })
//                 }
//             })
//         }

//     })
//     return newInt;
// }

function arr(ints){

    let newInt = [];

    ints.forEach((i)=>{
        if(typeof i === 'number'){
            newInt.push(i);
        }else{
           
            let newArr = arr(i);
            newArr.forEach((ii)=>{
                newInt.push(ii);
            })
        
            
        }

    })
    return newInt;
}



console.log(arr([1,2,[3,4,[5,6,7],8],9,10]));