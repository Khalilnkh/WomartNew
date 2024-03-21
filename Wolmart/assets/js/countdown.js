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

//Scroll back to top

let progressPath = document.getElementById("progress-path");
let progressWrap = document.getElementById("progress-wrap");
let pathLength = progressPath.getTotalLength();
progressPath.style.transition = progressPath.style.webkitTransition = "none";
progressPath.style.strokeDasharray = pathLength + " " + pathLength;
progressPath.style.strokeDashoffset = pathLength;
progressPath.getBoundingClientRect();
progressPath.style.transition = progressPath.style.webkitTransition =
  "stroke-dashoffset 10ms linear";
  
const onScollEvent = function (event) {
  let scroll = window.scrollY;
  let height = document.body.scrollHeight - window.innerHeight;
  let progress = pathLength - (scroll * pathLength) / height;
  progressPath.style.strokeDashoffset = progress;

  let offset = 50;
  if (window.scrollY > offset) {
    progressWrap.classList.add("active-progress");
  } else {
    progressWrap.classList.remove("active-progress");
  }
};

onScollEvent();
window.onscroll = onScollEvent;
progressWrap.onclick = function (event) {
  window.scroll({ top: 0, behavior: "smooth" });
  return false;
};
