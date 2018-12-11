#include "stdafx.h"
#include <gtest\gtest.h>
extern int Add(int a, int b);

TEST(testCase, test0)
{
	EXPECT_EQ(14, Add(4, 10));
}

TEST(testCase, test1)
{
	EXPECT_EQ(6, Add(5, 7));
}

TEST(testCase, test2)
{
	EXPECT_EQ(28, Add(10, 18));
}