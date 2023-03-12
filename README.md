# Reverse Nonogram API

## What's a nonogram?
From [Wikipedia](https://en.wikipedia.org/wiki/Nonogram):
> Nonograms, also known as Hanjie, Paint by Numbers, Picross, Griddlers, and Pic-a-Pix, and by various other names, are picture logic puzzles in which cells in a grid must be colored or left blank according to numbers at the side of the grid to reveal a hidden pixel art-like picture. In this puzzle type, the numbers are a form of discrete tomography that measures how many unbroken lines of filled-in squares there are in any given row or column. For example, a clue of "4 8 3" would mean there are sets of four, eight, and three filled squares, in that order, with at least one blank square between successive sets.

## What does this API do?
With a nonogram, the goal is to recreate a simple picture on a blank grid using the provided clues. I built this api to take a simple image and return the set of clues necessary to reconstruct that image - hence, the "Reverse Nonogram API".

## How do I run this?
To run this using Docker, simply build and run:

```
docker build -t rna .
docker run -p 5000:80 rna
```

The Swagger documentation should then be available at `http://localhost:5000/index.html`