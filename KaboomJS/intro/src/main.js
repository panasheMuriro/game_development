import kaboom from "kaboom"


// Initialize Kaboom
kaboom(
	{
		// width: 320,
		// height: 240,
		font: "sans-serif",
		canvas: document.querySelector("#mycanvas"),
		background: [ 120, 30, 67, ],
	}
);
loadSprite("bean", "sprites/bean.png")

setGravity(2400)
// Define the player (bean sprite)
const bean = add([
    sprite("bean"),  // This would be your bean image or sprite asset
    pos(80, 80),     // Position the sprite
    area(),          // Define the area for collision detection
    body(),          // Enable physics
]);

// Define the gro
// Jumping functionality
onKeyPress("space", () => {
    // .jump() is provided by the body() component
    bean.jump()
})


// const k = kaboom()

// setGravity(2400)

// k.loadSprite("bean", "sprites/bean.png")
// // 
// k.add([
// 	k.pos(120, 80),
// 	k.sprite("bean"),
// ])

// // compose the player game object from multiple components and add it to the game
// const bean = add([
//     sprite("bean"),
//     pos(80, 40),
//     area(),
//     body(),
// ])

// // press space to jump
// onKeyPress("space", () => {
//     // this method is provided by the "body" component above
//     bean.jump()
// })

// add([
//     text("oh hi"),
//     pos(80, 40),
// ])

// add([
//     rect(width(), 48),    // A simple rectangle for the ground
//     pos(0, height() - 48), // Position it at the bottom of the screen
//     area(),
//     solid(),             // Make it solid for collision
//     color(0, 255, 0),    // Ground color
// ]);



// k.onClick(() => k.addKaboom(k.mousePos()))