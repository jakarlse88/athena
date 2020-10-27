using Microsoft.EntityFrameworkCore.Migrations;

namespace Athena.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormFamily",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFamily", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RelativeDirection",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelativeDirection", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RotationCategory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotationCategory", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StanceCategory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceCategory", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StanceType",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceType", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TechniqueCategory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechniqueCategory", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TechniqueType",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechniqueType", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TransitionCategory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionCategory", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormFamilyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Form_FormFamily",
                        column: x => x.FormFamilyName,
                        principalTable: "FormFamily",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stance",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StanceCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StanceTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stance", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Stance_StanceCategory",
                        column: x => x.StanceCategoryName,
                        principalTable: "StanceCategory",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stance_StanceType",
                        column: x => x.StanceTypeName,
                        principalTable: "StanceType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Technique",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TechniqueTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TechniqueCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameHangeul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameHanja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technique_1", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Technique_TechniqueCategory",
                        column: x => x.TechniqueCategoryName,
                        principalTable: "TechniqueCategory",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Technique_TechniqueType",
                        column: x => x.TechniqueTypeName,
                        principalTable: "TechniqueType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RotationCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RelativeDirectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StanceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TechniqueName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transition_RelativeDirection",
                        column: x => x.RelativeDirectionName,
                        principalTable: "RelativeDirection",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transition_RotationCategory",
                        column: x => x.RotationCategoryName,
                        principalTable: "RotationCategory",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transition_Technique",
                        column: x => x.TechniqueName,
                        principalTable: "Technique",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StanceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TechniqueName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movement_Stance",
                        column: x => x.StanceName,
                        principalTable: "Stance",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Technique",
                        column: x => x.TechniqueName,
                        principalTable: "Technique",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Transition",
                        column: x => x.TransitionId,
                        principalTable: "Transition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransitionCategoryTransition",
                columns: table => new
                {
                    TransitionCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionCategoryTransition", x => new { x.TransitionCategoryName, x.TransitionId });
                    table.ForeignKey(
                        name: "FK_TransitionCategoryTransition_Transition",
                        column: x => x.TransitionId,
                        principalTable: "Transition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitionCategoryTransition_TransitionCategory",
                        column: x => x.TransitionCategoryName,
                        principalTable: "TransitionCategory",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NumberInSequence",
                columns: table => new
                {
                    FormName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovementId = table.Column<int>(type: "int", nullable: false),
                    OrdinalNumber = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberInSequence", x => new { x.FormName, x.MovementId, x.OrdinalNumber });
                    table.ForeignKey(
                        name: "FK_NumberInSequence_Form",
                        column: x => x.FormName,
                        principalTable: "Form",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NumberInSequence_Movement",
                        column: x => x.MovementId,
                        principalTable: "Movement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Form_FormFamilyName",
                table: "Form",
                column: "FormFamilyName");

            migrationBuilder.CreateIndex(
                name: "UK_Form_NameHangeul",
                table: "Form",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_Form_NameHanja",
                table: "Form",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_FormFamily_NameHangeul",
                table: "FormFamily",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_FormFamily_NameHanja",
                table: "FormFamily",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_TechniqueName",
                table: "Movement",
                column: "TechniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_TransitionId",
                table: "Movement",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "UK_Movement",
                table: "Movement",
                columns: new[] { "StanceName", "TechniqueName", "TransitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumberInSequence_MovementId",
                table: "NumberInSequence",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "UK_RelativeDirection_NameHangeul",
                table: "RelativeDirection",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_RelativeDirection_NameHanja",
                table: "RelativeDirection",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_RotationCategory_NameHangeul",
                table: "RotationCategory",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_RotationCategory_NameHanja",
                table: "RotationCategory",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Stance_StanceCategoryName",
                table: "Stance",
                column: "StanceCategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Stance_StanceTypeName",
                table: "Stance",
                column: "StanceTypeName");

            migrationBuilder.CreateIndex(
                name: "UK_Stance",
                table: "Stance",
                columns: new[] { "Name", "StanceCategoryName", "StanceTypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Stance_NameHangeul",
                table: "Stance",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_Stance_NameHanja",
                table: "Stance",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_StanceCategory_NameHangeul",
                table: "StanceCategory",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_StanceCategory_NameHanja",
                table: "StanceCategory",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_StanceType_NameHangeul",
                table: "StanceType",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_StanceType_NameHanja",
                table: "StanceType",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Technique_TechniqueCategoryName",
                table: "Technique",
                column: "TechniqueCategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Technique_TechniqueTypeName",
                table: "Technique",
                column: "TechniqueTypeName");

            migrationBuilder.CreateIndex(
                name: "UK_Technique",
                table: "Technique",
                columns: new[] { "Name", "TechniqueCategoryName", "TechniqueTypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Technique_NameHangeul",
                table: "Technique",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_Technique_NameHanja",
                table: "Technique",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_TechniqueCategory_NameHangeul",
                table: "TechniqueCategory",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_TechniqueCategory_NameHanja",
                table: "TechniqueCategory",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_TechniqueType_NameHangeul",
                table: "TechniqueType",
                column: "NameHangeul",
                unique: true,
                filter: "([NameHangeul] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UK_TechniqueType_NameHanja",
                table: "TechniqueType",
                column: "NameHanja",
                unique: true,
                filter: "([NameHanja] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_RotationCategoryName",
                table: "Transition",
                column: "RotationCategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_TechniqueName",
                table: "Transition",
                column: "TechniqueName");

            migrationBuilder.CreateIndex(
                name: "UK_Transition",
                table: "Transition",
                columns: new[] { "RelativeDirectionName", "RotationCategoryName", "StanceName", "TechniqueName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransitionCategoryTransition_TransitionId",
                table: "TransitionCategoryTransition",
                column: "TransitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberInSequence");

            migrationBuilder.DropTable(
                name: "TransitionCategoryTransition");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropTable(
                name: "TransitionCategory");

            migrationBuilder.DropTable(
                name: "FormFamily");

            migrationBuilder.DropTable(
                name: "Stance");

            migrationBuilder.DropTable(
                name: "Transition");

            migrationBuilder.DropTable(
                name: "StanceCategory");

            migrationBuilder.DropTable(
                name: "StanceType");

            migrationBuilder.DropTable(
                name: "RelativeDirection");

            migrationBuilder.DropTable(
                name: "RotationCategory");

            migrationBuilder.DropTable(
                name: "Technique");

            migrationBuilder.DropTable(
                name: "TechniqueCategory");

            migrationBuilder.DropTable(
                name: "TechniqueType");
        }
    }
}
