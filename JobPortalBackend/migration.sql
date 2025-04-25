CREATE TABLE "Favorites" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Favorites" PRIMARY KEY AUTOINCREMENT,
    "UserId" INTEGER NOT NULL,
    "JobId" INTEGER NOT NULL
);


CREATE TABLE "Jobs" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Jobs" PRIMARY KEY AUTOINCREMENT,
    "JobTitle" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Location" TEXT NOT NULL,
    "CompanyName" TEXT NOT NULL,
    "JobType" TEXT NOT NULL,
    "SalaryRange" TEXT NOT NULL,
    "ApplicationDeadline" TEXT NOT NULL,
    "PostedDate" TEXT NOT NULL
);


CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "Username" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "Phone" TEXT NOT NULL
);


