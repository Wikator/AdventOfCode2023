import { readFileSync } from 'fs'

function getLines (filePath: string): number[][] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\r\n').map(line => line.split('').map(c => parseInt(c)));
}

enum Direction {
    Up,
    Down,
    Right,
    Left
}

class Neighbor {
    row: number;
    col: number;
    direction: Direction;

    constructor(row: number, col: number, direction: Direction) {
        this.row = row;
        this.col = col;
        this.direction = direction;
    }
}

function dijkstra2D(grid: number[][]): number {
    const INF: number = Number.MAX_SAFE_INTEGER;
    const rows: number = grid.length;
    const cols: number = grid[0].length;
    const distances: number[][] = new Array(rows).fill([]).map(() => new Array(cols).fill(INF));
    const visited: boolean[][] = new Array(rows).fill([]).map(() => new Array(cols).fill(false));

    distances[0][0] = grid[0][0];

    let straightLineLengthUp = 0;
    let straightLineLengthDown = 0;
    let straightLineLengthLeft = 0;
    let straightLineLengthRight = 0;

    let prevRow: number | null = null;
    let prevCol: number | null = null;

    for (let i: number = 0; i < rows * cols - 1; i++) {
        let row;
        let col;
        if (prevCol === null || prevRow === null) {
            [row, col] = minDistance2D(distances, visited);
            visited[row][col] = true;
        } else {
            [row, col] = minDistance2D(distances, visited);

            if (row === prevRow + 1) {
                if (straightLineLengthDown >= 3) {
                    console.log('got here');
                    [row, col] = minDistance2D2(distances, visited);
                } else {
                    straightLineLengthDown++;
                }
                straightLineLengthLeft = 0;
                straightLineLengthRight = 0;
                straightLineLengthUp = 0;
            }

            if (row === prevRow - 1) {
                if (straightLineLengthUp >= 3) {
                    console.log('got here');
                    [row, col] = minDistance2D2(distances, visited);
                } else {
                    straightLineLengthUp++;
                }
                straightLineLengthLeft = 0;
                straightLineLengthRight = 0;
                straightLineLengthDown = 0;
            }

            if (col === prevCol + 1) {
                if (straightLineLengthRight >= 3) {
                    console.log('got here');
                    [row, col] = minDistance2D2(distances, visited);
                } else {
                    straightLineLengthRight++;
                }
                straightLineLengthLeft = 0;
                straightLineLengthDown = 0;
                straightLineLengthUp = 0;
            }

            if (col === prevCol - 1) {
                if (straightLineLengthLeft >= 3) {
                    console.log('got here');
                    [row, col] = minDistance2D2(distances, visited);
                } else {
                    straightLineLengthLeft++;
                }
                straightLineLengthDown = 0;
                straightLineLengthRight = 0;
                straightLineLengthUp = 0;
            }

            visited[row][col] = true;
        }

        prevRow = row;
        prevCol = col;

        const neighbors: Neighbor[] = [
            new Neighbor(row - 1, col, Direction.Up),
            new Neighbor(row + 1, col, Direction.Down),
            new Neighbor(row, col - 1, Direction.Left),
            new Neighbor(row, col + 1, Direction.Right),
        ];

        for (const n of neighbors) {
            if (n.row >= 0 && n.row < rows && n.col >= 0 && n.col < cols && !visited[n.row][n.col]) {
                const newDistance: number = distances[row][col] + grid[n.row][n.col];
                if (newDistance < distances[n.row][n.col]) {
                    switch (n.direction) {
                        case Direction.Down:
                            if (straightLineLengthDown < 3) {
                                distances[n.row][n.col] = newDistance;
                            } else {
                                distances[n.row][n.col] = 200
                            }
                            break;
                        case Direction.Left:
                            if (straightLineLengthLeft < 3) {
                                distances[n.row][n.col] = newDistance;
                            } else {
                                distances[n.row][n.col] = 200
                            }
                            break;
                        case Direction.Right:
                            if (straightLineLengthRight < 3) {
                                distances[n.row][n.col] = newDistance;
                            } else {
                                distances[n.row][n.col] = 200
                            }
                            break;
                        case Direction.Up:
                            if (straightLineLengthUp < 3) {
                                distances[n.row][n.col] = newDistance;
                            } else {
                                distances[n.row][n.col] = 200
                            }
                            break;
                    }
                    // distances[n.row][n.col] = newDistance;
                }
            }
        }
    }

    return distances[rows - 1][cols - 1];
}

function minDistance2D(distances: number[][], visited: boolean[][]): [number, number] {
    let min: number = Number.MAX_SAFE_INTEGER;
    let minIndex: [number, number] = [-1, -1];

    for (let i: number = 0; i < distances.length; i++) {
        for (let j: number = 0; j < distances[i].length; j++) {
            if (!visited[i][j] && distances[i][j] < min) {
                min = distances[i][j];
                minIndex = [i, j];
            }
        }
    }

    return minIndex;
}

function minDistance2D2(distances: number[][], visited: boolean[][]): [number, number] {
    let min: number = Number.MAX_SAFE_INTEGER;
    let secondMin: number = Number.MAX_SAFE_INTEGER;
    let minIndex: [number, number] = [-1, -1];

    for (let i: number = 0; i < distances.length; i++) {
        for (let j: number = 0; j < distances[i].length; j++) {
            if (!visited[i][j] && distances[i][j] < secondMin) {
                if (!visited[i][j] && distances[i][j] < min) {
                    min = distances[i][j];
                } else {
                    secondMin = distances[i][j];
                    minIndex = [i, j];
                }
            }
        }
    }

    return minIndex;
}

const grid: number[][] = getLines('data/input.txt');

const result: number = dijkstra2D(grid);

console.log(result);
