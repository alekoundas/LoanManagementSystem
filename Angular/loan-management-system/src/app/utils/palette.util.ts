export class PaletteUtil {
  static getColor(index: number): string {
    const colors = ['#FF5733', '#33FF57', '#3357FF', '#FF33A1', '#A133FF'];
    return colors[index % colors.length];
  }
}
