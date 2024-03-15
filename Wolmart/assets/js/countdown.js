var count = 25253800;
var counter = setInterval(timer, 1000);
function timer() {
  count = count - 1;
  if (count <= 0) {
    clearInterval(counter);
    return;
  }
  let varDay  = Math.floor(count / 86400)
  let varHour = Math.floor(count / 3600) % 24
  let varMin = Math.floor(count / 60) % 60
  let varSec = count % 60
      
  countD = varDay;
  
  if (varHour >= 10) {
    countH = varHour;
  } else {
    countH = '0' + varHour;
  }

  if (varMin >= 10) {
    countM = varMin;
  } else {
    countM = '0' + varMin;
  }

  if (varSec == 60) {
    countS = '00';
  }else if(varSec >= 10) {
    countS = varSec;
  } else {
    countS = '0' + varSec;
  }

  document.getElementById("timer").innerHTML = countD + ':' + countH + ':' + countM + ':' + countS;
}