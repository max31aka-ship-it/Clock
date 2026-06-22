<?php
// clock.php - Аналоговые часы на PHP (веб-сервер)
// Отдаёт страницу с Canvas и JavaScript
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>🕐 ClockForge - PHP</title>
    <style>
        body{display:flex;justify-content:center;align-items:center;min-height:100vh;background:#2c3e50;margin:0;font-family:system-ui;}
        .container{background:#ecf0f1;padding:20px;border-radius:16px;box-shadow:0 10px 30px rgba(0,0,0,0.3);text-align:center;}
        canvas{border-radius:50%;box-shadow:0 0 0 4px #bdc3c7;background:#f5f5dc;cursor:pointer;}
        .controls{margin-top:15px;display:flex;gap:10px;justify-content:center;flex-wrap:wrap;}
        .controls button{padding:6px 12px;border:none;border-radius:6px;background:#3498db;color:white;cursor:pointer;}
        .controls button:hover{background:#2980b9;}
    </style>
</head>
<body>
<div class="container">
    <h2>🕐 ClockForge · PHP</h2>
    <canvas id="clockCanvas" width="400" height="400"></canvas>
    <div class="controls">
        <button id="bgBtn">Цвет фона</button>
        <button id="handBtn">Цвет стрелок</button>
        <button id="romanBtn">Римские цифры</button>
    </div>
</div>
<script>
const canvas=document.getElementById('clockCanvas');
const ctx=canvas.getContext('2d');
const W=400,H=400,cx=W/2,cy=H/2,radius=180;
let faceColor='#f5f5dc', handColor='#2c3e50', secondColor='#e74c3c', useRoman=false;

function drawClock(){
ctx.clearRect(0,0,W,H);
ctx.beginPath(); ctx.arc(cx,cy,radius,0,2*Math.PI);
ctx.fillStyle=faceColor; ctx.fill();
ctx.strokeStyle='#2c3e50'; ctx.lineWidth=2; ctx.stroke();
const now=new Date();
const hours=now.getHours()%12, minutes=now.getMinutes(), seconds=now.getSeconds();
for(let i=0;i<12;i++){
let angle=(i*30-90)*Math.PI/180;
let x1=cx+(radius-20)*Math.cos(angle), y1=cy+(radius-20)*Math.sin(angle);
let x2=cx+(radius-5)*Math.cos(angle), y2=cy+(radius-5)*Math.sin(angle);
ctx.beginPath(); ctx.moveTo(x1,y1); ctx.lineTo(x2,y2);
ctx.strokeStyle='#2c3e50'; ctx.lineWidth=3; ctx.stroke();
let label;
if(useRoman){ const roman=['XII','I','II','III','IV','V','VI','VII','VIII','IX','X','XI']; label=roman[i];}
else{ label=(i+1)===12?'12':(i+1); }
let tx=cx+(radius-40)*Math.cos(angle), ty=cy+(radius-40)*Math.sin(angle);
ctx.fillStyle='#2c3e50'; ctx.font='bold 16px Arial'; ctx.textAlign='center'; ctx.textBaseline='middle';
ctx.fillText(label,tx,ty);
}
function drawHand(angle,length,color,width){ let x=cx+length*Math.cos(angle), y=cy+length*Math.sin(angle);
ctx.beginPath(); ctx.moveTo(cx,cy); ctx.lineTo(x,y);
ctx.strokeStyle=color; ctx.lineWidth=width; ctx.lineCap='round'; ctx.stroke(); }
let hourAngle=((hours+minutes/60)*30-90)*Math.PI/180;
drawHand(hourAngle,radius*0.5,handColor,6);
let minAngle=((minutes+seconds/60)*6-90)*Math.PI/180;
drawHand(minAngle,radius*0.7,handColor,4);
let secAngle=(seconds*6-90)*Math.PI/180;
drawHand(secAngle,radius*0.8,secondColor,2);
ctx.beginPath(); ctx.arc(cx,cy,8,0,2*Math.PI); ctx.fillStyle='#2c3e50'; ctx.fill();
}
function updateClock(){ drawClock(); requestAnimationFrame(updateClock); }
document.getElementById('bgBtn').onclick=()=>{ let c=prompt('Цвет фона (HEX):',faceColor); if(c) faceColor=c; };
document.getElementById('handBtn').onclick=()=>{ let c=prompt('Цвет стрелок (HEX):',handColor); if(c) handColor=c; };
document.getElementById('romanBtn').onclick=()=>{ useRoman=!useRoman; };
updateClock();
</script>
</body>
</html>
