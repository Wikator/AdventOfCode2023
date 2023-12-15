package day14

import scala.annotation.tailrec
import scala.io.Source


def moveAllDown(lines: Array[String]): Array[String] = {
  moveAllUp(lines.reverse).reverse
}

def moveAllRight(lines: Array[String]): Array[String] = {
  moveAllLeft(lines.map(l => l.reverse)).map(l => l.reverse)
}

@tailrec
def moveAllLeft(lines: Array[String], acc: List[String] = List()): Array[String] = {
  if (acc.isEmpty)
    moveAllLeft(lines, lines.map(_ => "").toList)
  else
    if(lines(0).isEmpty)
      acc.toArray
    else
      val osToDelete: List[OToDelete] = lines
        .zipWithIndex
        .foldLeft(List[OToDelete]())((acc, curr) =>
          if (curr._1(0) == '.')
            findOToDeleteRight(curr._1, curr._2) match
              case Some(value) => value :: acc
              case None => acc
          else
            acc
        )

      val newAcc: Array[String] = lines.zipWithIndex.map((line, index1) =>
        if (line.head == '.' && oExistsRight(line.tail))
          acc.zipWithIndex.foldLeft("")((acc, curr) => if index1 == curr._2 && acc == "" then s"${curr._1}O" else acc)
        else
          acc.zipWithIndex.foldLeft("")((acc, curr) => if index1 == curr._2 && acc == "" then s"${curr._1}${line.head}" else acc)
      )

      val newLines = lines.zipWithIndex.map((line, index1) =>
        line.tail.zipWithIndex.map((char, index2) =>
          if (osToDelete.exists(o => o.line == index1 && o.index == index2 + 1))
            '.'
          else
            char
        ).foldLeft("")((acc, curr) => acc + curr))

      moveAllLeft(newLines, newAcc.toList)
}


@tailrec
def findOToDeleteRight(line: String, lineIndex: Int, acc: Int = 0): Option[OToDelete] = {
  if (line.isEmpty)
    None
  else
    line.head match
      case 'O' => Some(OToDelete(lineIndex, acc))
      case '#' => None
      case '.' => findOToDeleteRight(line.tail, lineIndex, acc + 1)
}

@tailrec
def oExistsRight(line: String): Boolean = {
  if (line.isEmpty)
    false
  else
    line.head match
      case 'O' => true
      case '#' => false
      case '.' => oExistsRight(line.tail)
}

def performCycle(inputLines: Array[String]): Array[String] = {
  moveAllRight(moveAllDown(moveAllLeft(moveAllUp(inputLines))))
}

@tailrec
def repeatCycle(lines: Array[String], cyclesLeft: Int): Array[String] = {
  if (cyclesLeft <= 0)
    lines
  else
    repeatCycle(performCycle(lines), cyclesLeft - 1)
}

@tailrec
def findCycleLength(lines: Array[String], length: Int, allSums: List[Int] = List()): Unit = {
  val result = performCycle(lines)
  val currentSum = sum(result.reverse)

   if (allSums.contains(currentSum))
    println(s"${allSums.length} - $currentSum")

  if (length > 1)
    findCycleLength(result, length - 1, currentSum :: allSums)
}

@main
def Main2(): Unit = {
  // findCycleLength(inputLines, 400)
  val lines = repeatCycle(inputLines, 143 + ((1000000000 - 143) % 42))
  lines.foreach(l => println(l))
  println(sum(lines.reverse))
}
